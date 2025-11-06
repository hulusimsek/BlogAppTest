using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQueryValidator : AbstractValidator<GetPostsByCategoryQuery>
    {
        public GetPostsByCategoryQueryValidator()
        {
            RuleFor(x => x.categorySlug)
                .NotEmpty().WithMessage("Post bulunamadı.");

            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Sayfa numarası 1 veya daha büyük olmalıdır.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Sayfa başına gösterilecek içerik sayısı 1 ile 100 arasında olmalıdır.");
        }
    }
}
