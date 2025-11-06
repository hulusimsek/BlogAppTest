using BlogApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.PageSection.Commands.IncrementHomeViewCount
{
    public class IncrementViewCountCommand : IRequest<Result>
    {
        public Guid? Id { get; set; }
    }
}
