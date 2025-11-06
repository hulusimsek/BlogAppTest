using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactFormSettings.Commands.UpdateContactFormSettings
{
    public class UpdateContactFormSettingsCommand : IRequest<Result<ContactFormSettingsDto>>
    {
        public UpdateContactFormSettingsDto Data { get; set; } = null!;
    }
}