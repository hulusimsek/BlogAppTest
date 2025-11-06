using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.BlogPost
{
    public class UpdateBlogPostDto : CreateBlogPostDto
    {
        public Guid Id { get; set; }
    }
}
