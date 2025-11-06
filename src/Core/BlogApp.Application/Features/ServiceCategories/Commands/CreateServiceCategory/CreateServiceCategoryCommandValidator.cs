using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommandValidator : AbstractValidator<CreateServiceCategoryCommand>
    {
        public CreateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.Data).NotNull().WithMessage("Kategori verisi boş olamaz.");

            When(x => x.Data != null, () =>
            {
                RuleFor(x => x.Data.Name)
                    .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                    .MaximumLength(200).WithMessage("Kategori adı en fazla 200 karakter olmalıdır.");

                RuleFor(x => x.Data.DisplayOrder)
                    .GreaterThanOrEqualTo(0).WithMessage("Görüntüleme sırası negatif olamaz.");
            });
        }
    }

}
