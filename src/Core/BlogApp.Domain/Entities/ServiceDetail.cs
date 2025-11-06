using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class ServiceDetail : BaseEntity
    {
        public Guid ServiceCategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Subtitle { get; set; }
        public string? DetailedDescription { get; set; } // HTML destekli

        // Navigation Properties
        public ServiceCategory ServiceCategory { get; set; } = null!;

        public ICollection<ProcessFlowStep> ProcessFlowSteps { get; set; } = new List<ProcessFlowStep>();
        public ICollection<ServiceFaqItem> Faqs { get; set; } = new List<ServiceFaqItem>();
        public ICollection<TestimonialItem> Testimonials { get; set; } = new List<TestimonialItem>();

    }
}
