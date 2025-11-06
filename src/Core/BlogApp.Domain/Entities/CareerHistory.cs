using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class CareerHistory : BaseEntity
    {
        public Guid LawyerProfileId { get; set; }
        public string Position { get; set; } = string.Empty; // Örn: "Kurucu Avukat"
        public string Company { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty; // Örn: "2020"
        public string? EndDate { get; set; } // null ise "Günümüz"
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation Properties
        public LawyerProfile LawyerProfile { get; set; } = null!;
    }
}
