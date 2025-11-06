using BlogApp.Application.DTOs.BlogComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.BlogPost
{
    public class BlogPostDetailDto : BlogPostDto
    {
        public string Content { get; set; } = string.Empty;
        public List<BlogPostFaqItemDto> Faqs { get; set; } = new();
        public List<BlogCommentDto> Comments { get; set; } = new();
    }
}