using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.SiteSettings
{
    public class SiteSettingsDto
    {
        public Guid Id { get; set; }
        public string SiteName { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string FooterText { get; set; } = string.Empty;
        public string CopyrightText { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string LinkedInUrl { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string InstagramUrl { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonLink { get; set; } = string.Empty;
        public bool ButtonIsActive { get; set; } = true;
        public string ButtonAltText { get; set; } = string.Empty;

        public FaviconSetDto Favicons { get; set; } = new();
    }
}
