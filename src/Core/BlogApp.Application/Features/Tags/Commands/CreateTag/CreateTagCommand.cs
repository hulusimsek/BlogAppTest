using BlogApp.Application.Common;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.DTOs.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommand : IRequest<Result<TagDto>>
    {
        public CreateTagDto Data { get; set; } = null!;
    }
}
