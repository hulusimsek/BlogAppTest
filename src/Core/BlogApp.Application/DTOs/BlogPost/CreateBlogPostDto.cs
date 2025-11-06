using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.BlogPost
{
    public class CreateBlogPostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? FeaturedImageUrl { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string? AuthorImageUrl { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public bool IsPublished { get; set; }
        public List<Guid> Tags { get; set; } = new();
        public List<BlogPostFaqItemDto> BlogFaqItems { get; set; } = new();
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
    }
}
