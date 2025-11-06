using BlogApp.Application.Features.BlogPosts.Commands.DeleteBlogPost;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.MarkAsUnreplied
{
    public class MarkAsUnreadCommandValidator : AbstractValidator<MarkAsUnrepliedCommand>
    {
        public MarkAsUnreadCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("İletişim isteği bulunamadı.");
        }
    }
}
