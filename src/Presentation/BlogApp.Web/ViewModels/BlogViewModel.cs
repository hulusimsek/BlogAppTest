using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.DTOs.Tag;

namespace BlogApp.Web.ViewModels
{
    public class BlogViewModel
    {
        public PageSectionDto? Section { get; set; }
        public List<BlogPostDto> BlogPosts { get; set; } = new List<BlogPostDto>();
        public List<BlogPostDto> PopularBlogPosts { get; set; } = new List<BlogPostDto>();
        public List<MinimalCategoryDto> Categories { get; set; } = new List<MinimalCategoryDto>();
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
        public string? SearchTitle { get; set; }
        public string? SortBy { get; set; } = "PublishDate";
        public bool Descending { get; set; } = true;

        public string? CurrentCategorySlug { get; set; }
        public string? CurrentCategoryName { get; set; }
        public string? CurrentTag { get; set; }
        public string? CurrentTagName { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int TotalCount { get; set; } = 0;
        public int AllTotalCount { get; set; } = 0;
        public int PageSize { get; set; } = 9;

    }
}
