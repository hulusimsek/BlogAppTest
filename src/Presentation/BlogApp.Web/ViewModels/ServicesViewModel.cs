using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;

namespace BlogApp.Web.ViewModels
{
    public class ServicesViewModel
    {
        public PageSectionDto? Section { get; set; }
        public List<ServiceCategoryDto> ServiceCategories { get; set; } = new();
    }
}
