using BlogApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.MarkAsRead
{
    public class MarkAsUnreadCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
