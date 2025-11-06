using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(x => x.Data.Id)
                .NotEmpty().WithMessage("Tag ID bulunamadı.");

            RuleFor(x => x.Data.Name)
                .NotEmpty().WithMessage("Tag adı boş olamaz.")
                .MaximumLength(100).WithMessage("Tag adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.Data.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .MaximumLength(100).WithMessage("Slug 100 karakterden uzun olamaz.");
        }
    }

}
