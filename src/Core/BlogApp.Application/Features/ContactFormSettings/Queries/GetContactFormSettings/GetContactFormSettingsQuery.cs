using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactFormSettings.Queries.GetContactFormSettings
{
    public class GetContactFormSettingsQuery : IRequest<Result<ContactFormSettingsDto>>
    {
        public string SectionKey { get; set; } = null!;
    }
}
