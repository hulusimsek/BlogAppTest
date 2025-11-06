using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class AppointmentRequest : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public Guid? ServiceCategoryId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Notes { get; set; }
        public bool KvkkAccepted { get; set; }

        // Navigation Properties
        public ServiceCategory? ServiceCategory { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending = 0,
        Confirmed = 1,
        Completed = 2,
        Cancelled = 3
    }
}
