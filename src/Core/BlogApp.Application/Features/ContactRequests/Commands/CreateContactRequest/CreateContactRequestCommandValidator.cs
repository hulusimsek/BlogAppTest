using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.CreateContactRequest
{
    public class CreateContactRequestCommandValidator : AbstractValidator<CreateContactRequestCommand>
    {
        public CreateContactRequestCommandValidator()
        {
            RuleFor(x => x.Data.FullName)
                .NotEmpty().WithMessage("Ad Soyad boş olamaz.")
                .MaximumLength(100).WithMessage("Ad Soyad en fazla 100 karakter olabilir.");

            RuleFor(x => x.Data.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

            RuleFor(x => x.Data.Phone)
                .Matches(@"^\+?\d{10,15}$").When(x => !string.IsNullOrEmpty(x.Data.Phone))
                .WithMessage("Telefon numarası geçersiz formatta.");

            RuleFor(x => x.Data.Subject)
                .NotEmpty().WithMessage("Konu boş olamaz.")
                .MaximumLength(200).WithMessage("Konu en fazla 200 karakter olabilir.");

            RuleFor(x => x.Data.Message)
                .NotEmpty().WithMessage("Mesaj boş olamaz.")
                .MinimumLength(10).WithMessage("Mesaj en az 10 karakter olmalı.")
                .MaximumLength(2000).WithMessage("Mesaj en fazla 2000 karakter olabilir.");

            RuleFor(x => x.Data.ServiceCategoryId)
                .NotEmpty().WithMessage("Hizmet kategorisi seçmelisiniz.");

        }
    }
}
