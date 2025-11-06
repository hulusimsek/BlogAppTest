using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.BlogComment
{
    public class BlogCommentSummaryDto
    {
        public string AuthorName { get; set; } = string.Empty;
        public string? AuthorImageUrl { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; }
    }
}
