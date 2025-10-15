using BlogApp.Domain.Entities;
using System.Security.Claims;

namespace BlogApp.Application.Interfaces;

/// <summary>
/// Token Service Interface
/// </summary>
public interface ITokenService
{
    string GenerateAccessToken(AppUser user, IList<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);
}