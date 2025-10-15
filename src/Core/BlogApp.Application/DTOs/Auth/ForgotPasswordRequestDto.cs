namespace BlogApp.Application.DTOs.Auth;

/// <summary>
/// Forgot password request DTO
/// </summary>
public class ForgotPasswordRequestDto
{
    public string Email { get; set; } = string.Empty;
}