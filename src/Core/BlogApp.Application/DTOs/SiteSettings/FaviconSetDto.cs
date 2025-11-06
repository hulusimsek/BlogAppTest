using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.SiteSettings
{
    public class FaviconSetDto
    {
        public string Favicon16 { get; set; } = string.Empty;
        public string Favicon32 { get; set; } = string.Empty;
        public string Favicon180 { get; set; } = string.Empty;
        public string Favicon192 { get; set; } = string.Empty;
        public string Favicon512 { get; set; } = string.Empty;
    }
}
