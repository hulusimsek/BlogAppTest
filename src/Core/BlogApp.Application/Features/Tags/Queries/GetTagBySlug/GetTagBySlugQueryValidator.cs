using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Queries.GetTagBySlug
{
    public class GetTagBySlugQueryValidator : AbstractValidator<GetTagBySlugQuery>
    {
        public GetTagBySlugQueryValidator()
        {
            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug bulunamadı.")
                .MaximumLength(100).WithMessage("Slug 100 karakterden uzun olamaz.");
        }
    }
}
