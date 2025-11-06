using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.DTOs.SiteSettings;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.ViewModels
{
    public class AboutViewModel
    {
        public LawyerProfileDto? LawyerProfile { get; set; }
        public ContactInfoDto? ContactInfo { get; set; }
        public SiteSettingsDto? SiteSettings { get; set; }
    }
}
