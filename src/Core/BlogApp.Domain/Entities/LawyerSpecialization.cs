using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class LawyerSpecialization : BaseEntity
    {
        public Guid LawyerProfileId { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation Properties
        public LawyerProfile LawyerProfile { get; set; } = null!;
        public ServiceCategory ServiceCategory { get; set; } = null!;
    }
}
