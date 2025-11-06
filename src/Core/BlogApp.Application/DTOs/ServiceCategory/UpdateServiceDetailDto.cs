using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class UpdateServiceDetailDto
    {
        public Guid? Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? DetailedDescription { get; set; }
        public List<ProcessFlowStepDto> ProcessFlow { get; set; } = new();
        public List<ServiceFaqItemDto> Faqs { get; set; } = new();
        public List<TestimonialItemDto> Testimonials { get; set; } = new();
    }
}
