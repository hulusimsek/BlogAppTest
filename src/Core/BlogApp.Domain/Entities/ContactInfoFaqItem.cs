using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class ContactInfoFaqItem : BaseEntity
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;

        public Guid ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; } = null!;
    }
}
