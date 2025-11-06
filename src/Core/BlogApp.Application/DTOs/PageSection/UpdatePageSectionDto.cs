using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.PageSection
{
    public class UpdatePageSectionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TitleExplanation { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? AlternativeTitle { get; set; }
        public string? AlternativeTitleExplanation { get; set; }
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonLink { get; set; } = string.Empty;
        public string ButtonAltText { get; set; } = string.Empty;
        public string BackgroundImageUrl { get; set; } = string.Empty;
        public string BackgroundAltText { get; set; } = string.Empty;
        public bool ButtonIsActive { get; set; } = true;

        // SEO & Analytics
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
    }
}
