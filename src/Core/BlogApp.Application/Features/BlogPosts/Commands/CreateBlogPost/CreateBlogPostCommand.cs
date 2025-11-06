using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.CreateBlogPost
{
    public class CreateBlogPostCommand : IRequest<Result<BlogPostDto>>
    {
        public CreateBlogPostDto Data { get; set; } = null!;
    }
}
