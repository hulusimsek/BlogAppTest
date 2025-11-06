using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.UpdateBlogPost
{
    public class UpdateBlogPostCommand : IRequest<Result<BlogPostDto>>
    {
        public UpdateBlogPostDto Data { get; set; } = null!;
    }
}
