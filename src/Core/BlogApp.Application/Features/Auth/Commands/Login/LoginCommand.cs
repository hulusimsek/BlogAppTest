using MediatR;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;

namespace BlogApp.Application.Features.Auth.Commands.Login;

/// <summary>
/// Login Command
/// </summary>
public class LoginCommand : IRequest<Result<LoginResponseDto>>
{
    public string EmailOrUserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}