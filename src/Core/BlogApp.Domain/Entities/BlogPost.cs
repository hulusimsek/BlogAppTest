using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // HTML destekli
        public string? FeaturedImageUrl { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string? AuthorImageUrl { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public int ViewCount { get; set; }
        public int DisplayOrder { get; set; }

        // SEO
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }

        // Navigation Properties
        public Guid? ServiceCategoryId { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }
        public ICollection<BlogFaqItem> Faqs { get; set; } = new List<BlogFaqItem>();
        public ICollection<BlogComment> Comments { get; set; } = new List<BlogComment>();
        public ICollection<BlogPostTag> Tags { get; set; } = new List<BlogPostTag>();
    }
}
