using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.DTOs.Tag;

namespace BlogApp.Web.ViewModels
{
    public class BlogDetailViewModel
    {
        public BlogPostDetailDto BlogPost { get; set; } = new();
        public List<BlogPostDto> RelatedPosts { get; set; } = new();
        public List<BlogPostDto> PopularBlogPosts { get; set; } = new();
        public List<MinimalCategoryDto> Categories { get; set; } = new();
        public List<TagDto> Tags { get; set; } = new();
        public PageSectionDto? Section { get; set; }
    }
}