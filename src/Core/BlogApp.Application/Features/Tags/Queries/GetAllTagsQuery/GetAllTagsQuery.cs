using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Queries.GetAllTagsQuery
{
    public class GetAllTagsQuery : IRequest<Result<List<TagDto>>>
    {
    }
}
