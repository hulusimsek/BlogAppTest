using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetServiceCategoryById
{
    public class GetLawyerProfileByIdQueryValidator : AbstractValidator<GetServiceCategoryByIdQuery>
    {
        public GetLawyerProfileByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kategori ID'si boş olamaz.")
                .NotEqual(Guid.Empty).WithMessage("Geçersiz kategori ID'si.");
        }
    }
}
