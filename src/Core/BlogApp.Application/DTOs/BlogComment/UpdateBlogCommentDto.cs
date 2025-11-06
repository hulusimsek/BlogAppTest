using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.BlogComment
{
    public class UpdateBlogCommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
    }
}
