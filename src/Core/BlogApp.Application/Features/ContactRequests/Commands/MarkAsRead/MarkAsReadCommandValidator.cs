using BlogApp.Application.Features.BlogPosts.Commands.DeleteBlogPost;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.MarkAsRead
{
    public class MarkAsUnreadCommandValidator : AbstractValidator<MarkAsUnreadCommand>
    {
        public MarkAsUnreadCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("İletişim isteği bulunamadı.");
        }
    }
}
