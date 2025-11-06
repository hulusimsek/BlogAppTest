using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class CreateServiceCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; }
        public bool ShowOnHomePage { get; set; } = true;
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }

        // Yeni: isteğe bağlı detail. Frontend tek request'te gönderirse kullanılır.
        public CreateServiceDetailDto? ServiceDetail { get; set; }

    }
}
