using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetAllBlogPosts
{

    public class GetAllBlogPostsQuery : IRequest<Result<List<BlogPostDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool includeUnPublishedPost { get; set; } = false;
    }
}
