using FluentValidation;

namespace BlogApp.Application.Features.Auth.Commands.Login;

/// <summary>
/// Login Command Validator
/// </summary>
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.EmailOrUserName)
            .NotEmpty()
            .WithMessage("Email veya kullanıcı adı gereklidir");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Şifre gereklidir")
            .MinimumLength(6)
            .WithMessage("Şifre en az 6 karakter olmalıdır");
    }
}