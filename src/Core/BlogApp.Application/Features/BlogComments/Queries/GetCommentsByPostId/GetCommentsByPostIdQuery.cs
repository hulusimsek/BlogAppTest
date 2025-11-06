using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogComment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQuery : IRequest<Result<List<BlogCommentDto>>>
    {
        public Guid PostId { get; set; }
        public bool OnlyApproved { get; set; } = true;
    }
}
