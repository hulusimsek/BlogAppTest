using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostWithDetailById
{
    public class GetBlogPostWithDetailByIdQuery : IRequest<Result<BlogPostDetailDto>>
    {
        public Guid Id { get; set; }
    }
}
