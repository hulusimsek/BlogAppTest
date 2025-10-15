using MediatR;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;

namespace BlogApp.Application.Features.Auth.Commands.Register;

/// <summary>
/// Register Command
/// </summary>
public class RegisterCommand : IRequest<Result<LoginResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}