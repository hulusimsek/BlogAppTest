using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.SiteSettings;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.ViewModels
{
    public class ContactViewModel
    {
        public ContactInfoDto? ContactInfo { get; set; }
        public SiteSettingsDto? SiteSettings { get; set; }
        public PageSectionDto? Section { get; set; }
    }
}
