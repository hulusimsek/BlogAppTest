using MediatR;
using BlogApp.Application.Common;

namespace BlogApp.Application.Features.Auth.Commands.ForgotPassword;

/// <summary>
/// Forgot Password Command
/// </summary>
public class ForgotPasswordCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
}