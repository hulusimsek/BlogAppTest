using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.SiteSettings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactInfo.Queries.Commands
{
    public class GetContactInfoQuery : IRequest<Result<ContactInfoDto>>
    {
    }
}
