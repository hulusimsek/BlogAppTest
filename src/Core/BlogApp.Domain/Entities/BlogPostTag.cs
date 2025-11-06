using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class BlogPostTag : BaseEntity
    {
        public Guid BlogPostId { get; set; }
        public Guid TagId { get; set; }

        // Navigation Properties
        public BlogPost BlogPost { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
