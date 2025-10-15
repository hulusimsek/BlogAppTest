using FluentValidation;

namespace BlogApp.Application.Features.Auth.Commands.Register;

/// <summary>
/// Register Command Validator
/// </summary>
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email gereklidir")
            .EmailAddress()
            .WithMessage("Geçerli bir email adresi giriniz");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Kullanıcı adı gereklidir")
            .MinimumLength(3)
            .WithMessage("Kullanıcı adı en az 3 karakter olmalıdır")
            .MaximumLength(50)
            .WithMessage("Kullanıcı adı en fazla 50 karakter olabilir");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("İsim gereklidir")
            .MaximumLength(100)
            .WithMessage("İsim en fazla 100 karakter olabilir");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Soyisim gereklidir")
            .MaximumLength(100)
            .WithMessage("Soyisim en fazla 100 karakter olabilir");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Şifre gereklidir")
            .MinimumLength(8)
            .WithMessage("Şifre en az 8 karakter olmalıdır")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]")
            .WithMessage("Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Şifre tekrarı gereklidir")
            .Equal(x => x.Password)
            .WithMessage("Şifreler eşleşmiyor");
    }
}