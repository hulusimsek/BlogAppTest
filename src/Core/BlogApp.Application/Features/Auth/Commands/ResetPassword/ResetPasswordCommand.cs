using MediatR;
using BlogApp.Application.Common;

namespace BlogApp.Application.Features.Auth.Commands.ResetPassword;

/// <summary>
/// Reset Password Command
/// </summary>
public class ResetPasswordCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}