using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.IncrementViewCount
{
    public class IncrementViewCountCommandValidator : AbstractValidator<IncrementViewCountCommand>
    {
        public IncrementViewCountCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Post bulunamadı.");

        }
    }

}
