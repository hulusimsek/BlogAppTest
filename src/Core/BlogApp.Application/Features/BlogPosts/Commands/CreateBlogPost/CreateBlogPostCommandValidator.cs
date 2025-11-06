using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.CreateBlogPost
{
    public class IncrementViewCountCommandValidator : AbstractValidator<CreateBlogPostCommand>
    {
        public IncrementViewCountCommandValidator()
        {
            RuleFor(x => x.Data.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Data.Summary)
                .MaximumLength(1000).WithMessage("Özet en fazla 1000 karakter olabilir.");

            RuleFor(x => x.Data.ServiceCategoryId)
                .NotEmpty().WithMessage("Kategori seçilmelidir.");

            RuleFor(x => x.Data.Tags)
                .NotEmpty().WithMessage("En az bir etiket eklemelisiniz.");

            RuleFor(x => x.Data.MetaTitle)
                .MaximumLength(60).WithMessage("Meta başlık en fazla 60 karakter olabilir.");

            RuleFor(x => x.Data.MetaDescription)
                .MaximumLength(160).WithMessage("Meta açıklama en fazla 160 karakter olabilir.");

            RuleFor(x => x.Data.MetaKeywords)
                .MaximumLength(200).WithMessage("Meta anahtar kelimeler en fazla 200 karakter olabilir.");

        }
    }

}
