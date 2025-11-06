using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetServiceCategoryBySlug
{
    public class GetServiceCategoryBySlugQueryValidator : AbstractValidator<GetServiceCategoryBySlugQuery>
    {
        public GetServiceCategoryBySlugQueryValidator()
        {
            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug boş olamaz.");
        }
    }
}
