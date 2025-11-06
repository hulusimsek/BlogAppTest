using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class ProcessFlowStep : BaseEntity
    {
        public int StepNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;

        // Foreign Key
        public Guid ServiceDetailId { get; set; }

        // Navigation Property
        public ServiceDetail ServiceDetail { get; set; } = null!;
    }
}
