using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogComment;
using BlogApp.Application.DTOs.BlogPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.CreateBlogComment
{
    public class CreateBlogCommentCommand : IRequest<Result<BlogCommentDto>>
    {
        public CreateBlogCommentDto Data { get; set; } = null!;
    }
}
