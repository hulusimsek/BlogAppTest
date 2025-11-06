using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Tag;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommand : IRequest<Result<TagDto>>
    {
        public UpdateTagDto Data { get; set; } = null!;
    }
}
