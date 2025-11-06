using BlogApp.Application.Common;
using BlogApp.Application.DTOs.SiteSettings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.SiteSettings.Commands.UpdateSiteSettings
{
    public class UpdateSiteSettingsCommand : IRequest<Result<SiteSettingsDto>>
    {
        public Guid Id { get; set; }
        public UpdateSiteSettingsDto Data { get; set; } = null!;
        // IFormFile yerine Stream kullanıyoruz
        public Stream? FaviconStream { get; set; }
        public string? FaviconFileName { get; set; }
    }
}
