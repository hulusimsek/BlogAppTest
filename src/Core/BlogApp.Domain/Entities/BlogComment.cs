using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class BlogComment : BaseEntity
    {
        public Guid BlogPostId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public string? AuthorImageUrl { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
        public DateTime? CommentDate { get; set; }

        // Navigation Properties
        public BlogPost BlogPost { get; set; } = null!;
    }
}
