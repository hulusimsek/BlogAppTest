using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostBySlug
{
    public class GetBlogPostBySlugQuery : IRequest<Result<BlogPostDetailDto>>
    {
        public string Slug { get; set; } = string.Empty;
        public bool IncludeUnapprovedComments { get; set; } = false;
        public bool includeUnPublishedPost { get; set; } = false;

    }

}
