using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class TestimonialItemDto
    {
        public Guid? Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorLocation { get; set; } = string.Empty;
    }
}
