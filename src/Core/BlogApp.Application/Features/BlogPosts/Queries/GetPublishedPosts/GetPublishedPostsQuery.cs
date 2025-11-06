using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetPublishedPosts
{
    public class GetPublishedPostsQuery : IRequest<Result<List<BlogPostDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; } = "PublishDate"; // Varsayılan sıralama: PublishDate
        public bool IsDescending { get; set; } = true; // Varsayılan sıralama yönü: Azalan
    }
}
