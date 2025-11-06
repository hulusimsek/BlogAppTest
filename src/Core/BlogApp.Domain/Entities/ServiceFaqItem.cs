using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class ServiceFaqItem : BaseEntity
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;

        // Foreign Key
        public Guid ServiceDetailId { get; set; }

        // Navigation Property
        public ServiceDetail ServiceDetail { get; set; } = null!;
    }

}
