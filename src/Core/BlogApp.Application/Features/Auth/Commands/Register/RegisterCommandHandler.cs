using MediatR;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;
using BlogApp.Application.Interfaces;

namespace BlogApp.Application.Features.Auth.Commands.Register;

/// <summary>
/// Register Command Handler
/// </summary>
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<LoginResponseDto>>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<LoginResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(
            request.Email,
            request.UserName,
            request.FirstName,
            request.LastName,
            request.Password);
            
        return result;
    }
}