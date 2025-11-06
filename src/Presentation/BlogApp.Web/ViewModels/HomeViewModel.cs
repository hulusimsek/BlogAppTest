using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;

namespace BlogApp.Web.ViewModels
{
    public class HomeViewModel
    {
        public PageSectionDto? Section { get; set; }
        public List<ServiceCategoryDto> ServiceCategories { get; set; } = new();
        public List<BlogPostDto> LatestBlogPosts { get; set; } = new();
    }
}