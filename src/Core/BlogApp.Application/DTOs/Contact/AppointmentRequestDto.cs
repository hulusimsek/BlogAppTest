using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Contact
{
    public class AppointmentRequestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public Guid? ServiceCategoryId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool KvkkAccepted { get; set; }
    }
}
