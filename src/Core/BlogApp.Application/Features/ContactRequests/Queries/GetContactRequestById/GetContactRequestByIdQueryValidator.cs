using BlogApp.Application.Features.BlogPosts.Commands.DeleteBlogPost;
using BlogApp.Application.Features.ContactRequests.Queries.GetContactRequestById;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.GetContactRequestById
{
    public class MarkAsUnreadCommandValidator : AbstractValidator<GetContactRequestByIdQuery>
    {
        public MarkAsUnreadCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("İletişim isteği bulunamadı.");
        }
    }
}
