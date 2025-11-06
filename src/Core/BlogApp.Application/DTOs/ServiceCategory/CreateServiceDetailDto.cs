using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class CreateServiceDetailDto
    {
        public Guid ServiceCategoryId { get; set; }
        public List<ProcessFlowStepDto> ProcessFlow { get; set; } = new();
        public List<ServiceFaqItemDto> Faqs { get; set; } = new();
        public string? Subtitle { get; set; }
        public string? DetailedDescription { get; set; } // HTML destekli
        public List<TestimonialItemDto> Testimonials { get; set; } = new();
        public string? ImageUrl { get; set; }
    }
}
