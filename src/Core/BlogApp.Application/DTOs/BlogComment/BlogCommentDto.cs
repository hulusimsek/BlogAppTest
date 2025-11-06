using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.BlogComment
{
    public class BlogCommentDto
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public string? AuthorImageUrl { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
