using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQueryValidator : AbstractValidator<GetCommentsByPostIdQuery>
    {
        public GetCommentsByPostIdQueryValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post bulunamadı.");
        }
    }
}
