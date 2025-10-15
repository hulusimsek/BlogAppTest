using FluentValidation;

namespace BlogApp.Application.Features.Auth.Commands.ResetPassword;

/// <summary>
/// Reset Password Command Validator
/// </summary>
public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email gereklidir")
            .EmailAddress()
            .WithMessage("Geçerli bir email adresi giriniz");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Reset token gereklidir");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Yeni şifre gereklidir")
            .MinimumLength(8)
            .WithMessage("Şifre en az 8 karakter olmalıdır")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]")
            .WithMessage("Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Şifre tekrarı gereklidir")
            .Equal(x => x.NewPassword)
            .WithMessage("Şifreler eşleşmiyor");
    }
}