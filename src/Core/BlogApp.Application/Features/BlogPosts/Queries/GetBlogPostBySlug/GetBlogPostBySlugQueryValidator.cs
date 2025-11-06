using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostBySlug
{
    public class GetBlogPostBySlugQueryValidator : AbstractValidator<GetBlogPostBySlugQuery>
    {
        public GetBlogPostBySlugQueryValidator()
        {
            RuleFor(x => x.Slug)
                .NotEmpty()
                .WithMessage("Slug değeri boş olamaz.");
        }
    }
}
