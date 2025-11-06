using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Contact
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? MapEmbedUrl { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Yeni eklenen alanlar:
        public string StreetAddress { get; set; } = string.Empty; // Sokak adresi
        public string AddressLocality { get; set; } = string.Empty; // İlçe
        public string AddressRegion { get; set; } = string.Empty; // İl
        public string PostalCode { get; set; } = string.Empty; // Posta kodu
        public string AddressCountry { get; set; } = string.Empty; // Ülke

        public List<ContactInfoFaqItemDto> Faqs { get; set; } = new();

        public List<WorkingHourDto> WorkingHours { get; set; } = new();

    }
}