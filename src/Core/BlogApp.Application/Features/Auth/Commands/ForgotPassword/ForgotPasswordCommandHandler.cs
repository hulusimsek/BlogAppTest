using MediatR;
using BlogApp.Application.Common;
using BlogApp.Application.Interfaces;

namespace BlogApp.Application.Features.Auth.Commands.ForgotPassword;

/// <summary>
/// Forgot Password Command Handler
/// </summary>
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
{
    private readonly IAuthService _authService;

    public ForgotPasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.ForgotPasswordAsync(request.Email);
        return result;
    }
}