using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;
using BlogApp.Application.Interfaces;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using BlogApp.Persistence.Identity;

namespace BlogApp.Persistence.Services;

/// <summary>
/// Authentication Service Implementation
/// Domain AppUser ile Identity IdentityAppUser arasında köprü görevi görür
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityAppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<IdentityAppUser> userManager,
        ITokenService tokenService,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(string emailOrUserName, string password)
    {
        try
        {
            // Identity kullanıcısını bul
            var identityUser = await FindIdentityUserAsync(emailOrUserName);
            if (identityUser == null)
            {
                return Result<LoginResponseDto>.Failure("Kullanıcı adı veya şifre hatalı");
            }

            // Şifre kontrolü
            var passwordValid = await _userManager.CheckPasswordAsync(identityUser, password);
            if (!passwordValid)
            {
                return Result<LoginResponseDto>.Failure("Kullanıcı adı veya şifre hatalı");
            }

            // IdentityAppUser'ı domain AppUser'a map et
            var domainUser = _mapper.Map<AppUser>(identityUser);
            
            // Last login date'i güncelle (business logic)
            domainUser.SetLastLoginDate();
            
            // Bu değişikliği IdentityAppUser'a yansıt ve kaydet
            identityUser.LastLoginDate = domainUser.LastLoginDate;
            await _userManager.UpdateAsync(identityUser);

            // Token oluştur
            var roles = await _userManager.GetRolesAsync(identityUser);
            var token = _tokenService.GenerateAccessToken(domainUser, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var response = new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                User = new UserDto
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email!,
                    UserName = identityUser.UserName!,
                    FirstName = identityUser.FirstName,
                    LastName = identityUser.LastName,
                    FullName = $"{identityUser.FirstName} {identityUser.LastName}",
                    EmailConfirmed = identityUser.EmailConfirmed,
                    CreatedDate = identityUser.CreatedDate,
                    LastLoginDate = identityUser.LastLoginDate,
                    Roles = roles.ToList()
                }
            };

            return Result<LoginResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<LoginResponseDto>.Failure($"Giriş işlemi başarısız: {ex.Message}");
        }
    }

    public async Task<Result<LoginResponseDto>> RegisterAsync(string email, string userName, string firstName, string lastName, string password)
    {
        try
        {
            // Identity kullanıcısı oluştur
            var identityUser = new IdentityAppUser
            {
                Email = email,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(identityUser, password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<LoginResponseDto>.Failure(errors);
            }

            // Kullanıcıya varsayılan rol ata
            await _userManager.AddToRoleAsync(identityUser, "User");

            // IdentityAppUser'ı domain AppUser'a map et (for business logic)
            var domainUser = _mapper.Map<AppUser>(identityUser);

            // Email confirmation token oluştur ve gönder
            var emailToken = await _tokenService.GenerateEmailConfirmationTokenAsync(domainUser);
            await _emailService.SendEmailConfirmationAsync(email, emailToken);

            // Welcome email gönder
            await _emailService.SendWelcomeEmailAsync(email, firstName);

            // Token oluştur
            var roles = await _userManager.GetRolesAsync(identityUser);
            var token = _tokenService.GenerateAccessToken(domainUser, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var response = new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                User = new UserDto
                {
                    Id = identityUser.Id,
                    Email = email,
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    FullName = $"{firstName} {lastName}",
                    EmailConfirmed = false,
                    CreatedDate = identityUser.CreatedDate,
                    Roles = roles.ToList()
                }
            };

            return Result<LoginResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<LoginResponseDto>.Failure($"Kayıt işlemi başarısız: {ex.Message}");
        }
    }

    public async Task<Result> ForgotPasswordAsync(string email)
    {
        try
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                // Güvenlik için her durumda başarılı dön
                return Result.Success();
            }

            var domainUser = await _unitOfWork.Users.GetByEmailAsync(email);
            if (domainUser != null)
            {
                var resetToken = await _tokenService.GeneratePasswordResetTokenAsync(domainUser);
                await _emailService.SendPasswordResetAsync(email, resetToken);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Şifre sıfırlama işlemi başarısız: {ex.Message}");
        }
    }

    public async Task<Result> ResetPasswordAsync(string email, string token, string newPassword)
    {
        try
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }

            var result = await _userManager.ResetPasswordAsync(identityUser, token, newPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result.Failure(errors);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Şifre sıfırlama başarısız: {ex.Message}");
        }
    }

    public async Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(refreshToken);
            if (principal == null)
            {
                return Result<LoginResponseDto>.Failure("Geçersiz refresh token");
            }

            var userId = principal.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Result<LoginResponseDto>.Failure("Geçersiz token claims");
            }

            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
            {
                return Result<LoginResponseDto>.Failure("Kullanıcı bulunamadı");
            }

            var domainUser = await _unitOfWork.Users.GetByIdAsync(identityUser.Id);
            var roles = await _userManager.GetRolesAsync(identityUser);
            
            var newToken = _tokenService.GenerateAccessToken(domainUser!, roles);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var response = new LoginResponseDto
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                User = new UserDto
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email!,
                    UserName = identityUser.UserName!,
                    FirstName = identityUser.FirstName,
                    LastName = identityUser.LastName,
                    FullName = $"{identityUser.FirstName} {identityUser.LastName}",
                    EmailConfirmed = identityUser.EmailConfirmed,
                    CreatedDate = identityUser.CreatedDate,
                    LastLoginDate = identityUser.LastLoginDate,
                    Roles = roles.ToList()
                }
            };

            return Result<LoginResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<LoginResponseDto>.Failure($"Token yenileme başarısız: {ex.Message}");
        }
    }

    public Task<Result> LogoutAsync(string userId)
    {
        try
        {
            // JWT tabanlı sistemde logout client-side yapılır
            // Token blacklisting burada implementable
            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Failure($"Çıkış işlemi başarısız: {ex.Message}"));
        }
    }

    public async Task<Result> ConfirmEmailAsync(string email, string token)
    {
        try
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }

            var result = await _userManager.ConfirmEmailAsync(identityUser, token);
            if (!result.Succeeded)
            {
                return Result.Failure("Email onaylama başarısız");
            }

            // Domain kullanıcısını da güncelle
            var domainUser = await _unitOfWork.Users.GetByEmailAsync(email);
            if (domainUser != null)
            {
                domainUser.ConfirmEmail();
                await _unitOfWork.Users.UpdateAsync(domainUser);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Email onaylama başarısız: {ex.Message}");
        }
    }

    private async Task<IdentityAppUser?> FindIdentityUserAsync(string emailOrUserName)
    {
        if (emailOrUserName.Contains("@"))
        {
            return await _userManager.FindByEmailAsync(emailOrUserName);
        }
        return await _userManager.FindByNameAsync(emailOrUserName);
    }
}