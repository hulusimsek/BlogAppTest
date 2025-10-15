using FluentValidation;

namespace BlogApp.Application.Features.Auth.Commands.ForgotPassword;

/// <summary>
/// Forgot Password Command Validator
/// </summary>
public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email gereklidir")
            .EmailAddress()
            .WithMessage("Geçerli bir email adresi giriniz");
    }
}