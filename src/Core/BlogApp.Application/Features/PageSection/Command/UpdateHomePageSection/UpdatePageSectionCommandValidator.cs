using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.PageSection.Command.UpdateHomePageSection
{
    public class UpdatePageSectionCommandValidator : AbstractValidator<UpdatePageSectionCommand>
    {
        public UpdatePageSectionCommandValidator()
        {
            RuleFor(x => x.Data.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Data.Subtitle)
                .NotEmpty().WithMessage("Alt başlık boş olamaz.")
                .MaximumLength(300).WithMessage("Alt başlık en fazla 300 karakter olabilir.");

            RuleFor(x => x.Data.Content)
                .MaximumLength(1000).WithMessage("Misyon metni en fazla 1000 karakter olabilir.");

            RuleFor(x => x.Data.ButtonText)
                .NotEmpty().WithMessage("Buton metni boş olamaz.")
                .MaximumLength(100).WithMessage("Buton metni en fazla 100 karakter olabilir.");

            RuleFor(x => x.Data.ButtonLink)
                .MaximumLength(500).WithMessage("Buton linki en fazla 500 karakter olabilir.");

            RuleFor(x => x.Data.BackgroundImageUrl)
                .MaximumLength(1000).WithMessage("Resim URL'si en fazla 1000 karakter olabilir.");

            RuleFor(x => x.Data.BackgroundAltText)
                .NotEmpty().WithMessage("Resim alt metni boş olamaz.")
                .MaximumLength(200).WithMessage("Alt metin en fazla 200 karakter olabilir.");

            RuleFor(x => x.Data.MetaTitle)
                .MaximumLength(70).WithMessage("Meta başlık en fazla 70 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Data.MetaTitle));

            RuleFor(x => x.Data.MetaDescription)
                .MaximumLength(160).WithMessage("Meta açıklama en fazla 160 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Data.MetaDescription));

            RuleFor(x => x.Data.MetaKeywords)
                .MaximumLength(255).WithMessage("Meta anahtar kelimeler en fazla 255 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Data.MetaKeywords));
        }

    }
}
