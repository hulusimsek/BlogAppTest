using BlogApp.Application.Features.BlogPosts.Queries.GetPostsByCategory;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostWithDetailById
{
    public class GetBlogPostWithDetailByIdQueryValidator : AbstractValidator<GetPostsByCategoryQuery>
    {
        public GetBlogPostWithDetailByIdQueryValidator()
        {
            RuleFor(x => x.categorySlug)
                .NotEmpty().WithMessage("Post bulunamadı.");
        }
    }
}
