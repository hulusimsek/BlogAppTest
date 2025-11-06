using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.DeleteBlogPost
{
    public class DeleteBlogPostCommandValidator : AbstractValidator<DeleteBlogPostCommand>
    {
        public DeleteBlogPostCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Post bulunamadı.");
        }
    }
}
