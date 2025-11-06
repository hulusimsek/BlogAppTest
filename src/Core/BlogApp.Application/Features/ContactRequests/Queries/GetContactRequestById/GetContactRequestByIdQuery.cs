using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Queries.GetContactRequestById
{
    public class GetContactRequestByIdQuery : IRequest<Result<ContactRequestDto>>
    {
        public Guid Id { get; set; }
    }
}
