using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.ViewModels
{
    public class ServiceDetailViewModel
    {
        public ServiceCategoryDetailDto ServiceCategory { get; set; } = new();
        public PageSectionDto ServiceDetailPageSection { get; set; } = new();
        public ContactInfoDto? ContactInfo { get; set; } = new();
        public CreateContactRequestDto ContactRequest { get; set; } = new();
        public ContactFormSettingsDto ContactFormSettings { get; set; } = new();

    }
}
