using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class WorkingHour : BaseEntity
    {
        public string DayNameTr { get; set; } = string.Empty;
        public string DayNameEn { get; set; } = string.Empty;
        public string Opens { get; set; } = string.Empty; // "09:00"
        public string Closes { get; set; } = string.Empty; // "18:00"
        public bool IsClosed { get; set; }

        // Foreign Key
        public Guid ContactInfoId { get; set; }

        // Navigation
        public ContactInfo ContactInfo { get; set; } = null!;
    }
}
