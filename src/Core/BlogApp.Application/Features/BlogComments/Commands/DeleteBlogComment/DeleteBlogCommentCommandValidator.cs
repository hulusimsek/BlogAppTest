using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.DeleteBlogComment
{
    public class DeleteBlogCommentCommandValidator : AbstractValidator<DeleteBlogCommentCommand>
    {
        public DeleteBlogCommentCommandValidator()
        {
            RuleFor(x => x.CommentId)
                .NotEmpty().WithMessage("Yorum bulunamadı.");
        }
    }
}
