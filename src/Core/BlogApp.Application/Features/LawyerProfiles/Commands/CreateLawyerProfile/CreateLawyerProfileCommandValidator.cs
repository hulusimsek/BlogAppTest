using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Commands.CreateLawyerProfile
{
    public class CreateLawyerProfileCommandValidator : AbstractValidator<CreateLawyerProfileCommand>
    {
        public CreateLawyerProfileCommandValidator()
        {
            RuleFor(x => x.Data.FullName)
                .NotEmpty().WithMessage("Ad Soyad boş olamaz.")
                .MaximumLength(200).WithMessage("Ad Soyad en fazla 200 karakter olabilir.");

            RuleFor(x => x.Data.Title)
                .NotEmpty().WithMessage("Ünvan boş olamaz.")
                .MaximumLength(200).WithMessage("Ünvan en fazla 200 karakter olabilir.");

            RuleFor(x => x.Data.AboutText)
                .NotEmpty().WithMessage("Hakkında metni boş olamaz.");
        }
    }

}
