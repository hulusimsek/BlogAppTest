using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.ApproveBlogComment
{
    public class ApproveBlogCommentCommandValidator : AbstractValidator<ApproveBlogCommentCommand>
    {
        public ApproveBlogCommentCommandValidator()
        {
            RuleFor(x => x.CommentId)
                .NotEmpty().WithMessage("Yorum bulunamadı.");
        }
    }
}
