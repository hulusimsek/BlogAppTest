using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Commands.UpdateLawyerProfile
{
    public class UpdateLawyerProfileCommandValidator : AbstractValidator<UpdateLawyerProfileCommand>
    {
        public UpdateLawyerProfileCommandValidator()
        {
            RuleFor(x => x.Data.Id)
                .NotEmpty().WithMessage("Profil bulunamadı.");

            RuleFor(x => x.Data.FullName)
                .NotEmpty().WithMessage("Ad Soyad boş olamaz.")
                .MaximumLength(200).WithMessage("Ad Soyad en fazla 200 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Data.FullName));  // Eğer güncelleme varsa, zorunlu olabilir.

            RuleFor(x => x.Data.Title)
                .NotEmpty().WithMessage("Ünvan boş olamaz.")
                .MaximumLength(200).WithMessage("Ünvan en fazla 200 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Data.Title));  // Yalnızca boş değilse kontrol et.

            RuleFor(x => x.Data.AboutText)
                .NotEmpty().WithMessage("Hakkında metni boş olamaz.")
                .When(x => !string.IsNullOrEmpty(x.Data.AboutText)); // Eğer güncellenmişse, zorunlu olabilir.
        }
    }
}
