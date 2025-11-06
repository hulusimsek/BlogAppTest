using BlogApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.DeleteBlogComment
{
    public class DeleteBlogCommentCommand : IRequest<Result>
    {
        public Guid CommentId { get; set; }
    }
}
