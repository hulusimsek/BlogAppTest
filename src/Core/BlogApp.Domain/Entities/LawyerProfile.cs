using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class LawyerProfile : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty; // Örn: "Kurucu Avukat"
        public string ProfileImageUrl { get; set; } = string.Empty;
        public string AboutText { get; set; } = string.Empty; // HTML destekli
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public ICollection<LawyerSpecialization> Specializations { get; set; } = new List<LawyerSpecialization>();
        public ICollection<CareerHistory> CareerHistory { get; set; } = new List<CareerHistory>();
    }
}
