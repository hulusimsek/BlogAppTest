using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class ServiceDetailDto
    {
        public List<ProcessFlowStepDto> ProcessFlow { get; set; } = new();
        public List<ServiceFaqItemDto> Faqs { get; set; } = new();
        public string? DetailedDescription { get; set; } // HTML destekli
        public string? Subtitle { get; set; }
        public List<TestimonialItemDto> Testimonials { get; set; } = new();
        public string? ImageUrl { get; set; }
    }
}
