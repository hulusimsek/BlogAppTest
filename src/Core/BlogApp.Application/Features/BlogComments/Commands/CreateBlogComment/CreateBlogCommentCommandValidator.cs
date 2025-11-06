using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.CreateBlogComment
{
    public class CreateBlogCommentCommandValidator : AbstractValidator<CreateBlogCommentCommand>
    {
        public CreateBlogCommentCommandValidator()
        {
            RuleFor(x => x.Data.BlogPostId)
                .NotEmpty().WithMessage("Post bulunamadı.");

            RuleFor(x => x.Data.AuthorName)
                .NotEmpty().WithMessage("Yazar adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Yazar adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Data.AuthorEmail)
                .NotEmpty().WithMessage("Yazar e-postası boş bırakılamaz.")
                .EmailAddress().WithMessage("E-posta formatı hatalı.");

            RuleFor(x => x.Data.Content)
                .NotEmpty().WithMessage("Yorum içeriği boş olamaz.")
                .MaximumLength(1000).WithMessage("İçerik en fazla 1000 karakter olabilir.");
        }
    }

}
