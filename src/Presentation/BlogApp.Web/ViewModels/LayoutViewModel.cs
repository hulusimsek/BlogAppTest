using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Domain.Entities;

namespace BlogApp.Web.ViewModels
{
    public class LayoutViewModel
    {
        public SiteSettingsDto? SiteSettings { get; set; }
        public ContactInfoDto? ContactInfo { get; set; }
        public string? StructuredData { get; set; }
        public List<ServiceCategoryDto>? Services { get; set; }
    }
}
