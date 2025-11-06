using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.ServiceCategory;

namespace BlogApp.Web.ViewModels
{
    public class AppointmentViewModel
    {
        public CreateContactRequestDto ContactRequest { get; set; } = new();
        public ContactFormSettingsDto ContactFormSettings { get; set; } = new();
        public List<MinimalCategoryDto> Categories { get; set; } = new List<MinimalCategoryDto>();

    }
}
