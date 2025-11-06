using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Queries.GetTagBySlug
{
    public class GetTagBySlugQuery : IRequest<Result<TagDto>>
    {
        public string Slug { get; set; } = string.Empty;
    }
}
