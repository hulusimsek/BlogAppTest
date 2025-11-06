using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Contact
{
    public class UpdateContactFormSettingsDto
    {
        public Guid Id { get; set; }
        public string SmallBadgeText { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string SubtitleTemplate { get; set; } = string.Empty;

        public string LabelServiceCategory { get; set; } = string.Empty;
        public string LabelFullName { get; set; } = string.Empty;
        public string PlaceholderFullName { get; set; } = string.Empty;

        public string LabelEmail { get; set; } = string.Empty;
        public string PlaceholderEmail { get; set; } = string.Empty;

        public string LabelPhone { get; set; } = string.Empty;
        public string PlaceholderPhoneDisplay { get; set; } = string.Empty;
        public string PhonePrefixDisplay { get; set; } = string.Empty;

        public string LabelSubject { get; set; } = string.Empty;
        public string PlaceholderSubject { get; set; } = string.Empty;

        public string LabelMessage { get; set; } = string.Empty;
        public string PlaceholderMessage { get; set; } = string.Empty;

        public string KvkkTextTemplate { get; set; } = string.Empty;
        public string KvkkLink { get; set; } = string.Empty;

        public string SubmitButtonText { get; set; } = string.Empty;

        public bool RequireKvkkConsent { get; set; }
    }
}
