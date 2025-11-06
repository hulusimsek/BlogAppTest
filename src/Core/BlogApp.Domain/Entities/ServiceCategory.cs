using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class ServiceCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // Örn: "Ceza Hukuku"
        public string Slug { get; set; } = string.Empty; // URL için
        public string IconName { get; set; } = string.Empty; // Material icon adı
        public string ShortDescription { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public bool ShowOnHomePage { get; set; } = true;

        // SEO
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }

        // Navigation Properties
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public ServiceDetail? ServiceDetail { get; set; }

        // Burada LawyerSpecializations koleksiyonunu ekliyoruz
        public ICollection<LawyerSpecialization> LawyerSpecializations { get; set; } = new List<LawyerSpecialization>(); // Yeni ekleme

    }
}
