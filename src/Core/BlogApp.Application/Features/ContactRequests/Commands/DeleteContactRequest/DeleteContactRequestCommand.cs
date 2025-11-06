using BlogApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.DeleteContactRequest
{
    public class DeleteContactRequestCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
