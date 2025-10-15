using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;

namespace BlogApp.Application.Interfaces;

/// <summary>
/// Authentication Service Interface
/// Application katmanında tanımlanır, Infrastructure katmanında implement edilir
/// </summary>
public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(string emailOrUserName, string password);
    Task<Result<LoginResponseDto>> RegisterAsync(string email, string userName, string firstName, string lastName, string password);
    Task<Result> ForgotPasswordAsync(string email);
    Task<Result> ResetPasswordAsync(string email, string token, string newPassword);
    Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken);
    Task<Result> LogoutAsync(string userId);
    Task<Result> ConfirmEmailAsync(string email, string token);
}