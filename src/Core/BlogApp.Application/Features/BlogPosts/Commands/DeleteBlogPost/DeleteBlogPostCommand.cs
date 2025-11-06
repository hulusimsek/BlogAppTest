using BlogApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.DeleteBlogPost
{
    public class DeleteBlogPostCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
