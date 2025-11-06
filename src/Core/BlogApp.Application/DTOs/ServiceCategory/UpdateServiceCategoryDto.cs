using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class UpdateServiceCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool IsActive { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }

        public UpdateServiceDetailDto? ServiceDetail { get; set; }
    }
}
