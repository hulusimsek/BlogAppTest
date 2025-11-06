using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Contact
{
    public class ContactRequestDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        // Yeni ekleme: kullanıcı hangi hizmet için iletişim kurmuş
        public Guid? ServiceCategoryId { get; set; }
        public bool IsRead { get; set; }
        public bool IsReplied { get; set; }
        public DateTime? ReplyDate { get; set; }

    }
}
