using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactInfo.Commands.UpdateContactInfo
{
    public class UpdateContactInfoCommand : IRequest<Result<ContactInfoDto>>
    {
        public UpdateContactInfoDto Data { get; set; } = null!;
    }
}
