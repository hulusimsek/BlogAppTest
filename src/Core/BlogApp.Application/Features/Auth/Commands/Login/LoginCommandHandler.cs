using MediatR;
using Microsoft.AspNetCore.Identity;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;
using BlogApp.Application.Interfaces;

namespace BlogApp.Application.Features.Auth.Commands.Login;

/// <summary>
/// Login Command Handler
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request.EmailOrUserName, request.Password);
        return result;
    }
}