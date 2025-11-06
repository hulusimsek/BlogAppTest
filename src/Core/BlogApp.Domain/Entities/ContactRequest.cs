using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class ContactRequest : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsReplied { get; set; } = false;
        public DateTime? ReplyDate { get; set; }
        public bool KvkkAccepted { get; set; }

    }
}