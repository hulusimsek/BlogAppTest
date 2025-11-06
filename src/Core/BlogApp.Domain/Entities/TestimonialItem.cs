using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class TestimonialItem : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorLocation { get; set; } = string.Empty;

        // Foreign Key
        public Guid ServiceDetailId { get; set; }

        // Navigation Property
        public ServiceDetail ServiceDetail { get; set; } = null!;
    }

}
