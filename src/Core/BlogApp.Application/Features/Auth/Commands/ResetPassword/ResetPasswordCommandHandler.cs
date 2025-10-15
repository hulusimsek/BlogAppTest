using MediatR;
using BlogApp.Application.Common;
using BlogApp.Application.Interfaces;

namespace BlogApp.Application.Features.Auth.Commands.ResetPassword;

/// <summary>
/// Reset Password Command Handler
/// </summary>
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly IAuthService _authService;

    public ResetPasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.ResetPasswordAsync(
            request.Email,
            request.Token,
            request.NewPassword);
            
        return result;
    }
}