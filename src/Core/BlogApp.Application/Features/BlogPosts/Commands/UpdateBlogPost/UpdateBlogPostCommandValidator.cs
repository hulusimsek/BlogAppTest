using BlogApp.Application.DTOs.BlogPost;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.UpdateBlogPost
{
    public class UpdateBlogPostCommandValidator : AbstractValidator<UpdateBlogPostDto>
    {
        public UpdateBlogPostCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Post bulunamadı.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.ServiceCategoryId)
                .NotEmpty().WithMessage("Kategori seçilmelidir.");

            RuleFor(x => x.Tags)
                .Must(tags => tags.Count <= 10).WithMessage("En fazla 10 etiket eklenebilir.");

            RuleFor(x => x.MetaTitle)
                .MaximumLength(60).WithMessage("Meta başlık en fazla 60 karakter olabilir.");

            RuleFor(x => x.MetaDescription)
                .MaximumLength(160).WithMessage("Meta açıklama en fazla 160 karakter olabilir.");

            RuleFor(x => x.MetaKeywords)
                .MaximumLength(200).WithMessage("Meta anahtar kelimeler en fazla 200 karakter olabilir.");        }
    }

}
