using BlogApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
