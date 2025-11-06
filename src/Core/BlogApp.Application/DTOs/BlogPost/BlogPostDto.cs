using BlogApp.Application.DTOs.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.BlogPost
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string? FeaturedImageUrl { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string? AuthorImageUrl { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime? ModifiedDate { get; protected set; }
        public string? CategoryName { get; set; }
        public string? CategorySlug { get; set; }
        public int ViewCount { get; set; }
        public List<TagDto> Tags { get; set; } = new();

        // SEO Alanları
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
    }
}
