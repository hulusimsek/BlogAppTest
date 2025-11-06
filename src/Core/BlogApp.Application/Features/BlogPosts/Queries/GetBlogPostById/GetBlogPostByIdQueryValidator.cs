using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostById
{
    public class GetBlogPostByIdQueryValidator : AbstractValidator<GetBlogPostByIdQuery>
    {
        public GetBlogPostByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Post bulunamadı.");
        }
    }
}
