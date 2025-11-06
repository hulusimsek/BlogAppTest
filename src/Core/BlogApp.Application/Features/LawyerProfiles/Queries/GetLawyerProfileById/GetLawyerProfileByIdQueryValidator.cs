using BlogApp.Application.Features.LawyerProfiles.Queries.GetLawyerById;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetLawyerProfileById
{
    public class GetLawyerProfileByIdQueryValidator : AbstractValidator<GetLawyerProfileByIdQuery>
    {
        public GetLawyerProfileByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Profil ID'si boş olamaz.")
                .NotEqual(Guid.Empty).WithMessage("Geçersiz Profil ID'si.");
        }
    }
}
