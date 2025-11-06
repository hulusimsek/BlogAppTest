using BlogApp.Application.Common;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.LawyerProfile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.PageSection.Command.UpdateHomePageSection
{
    public class UpdatePageSectionCommand : IRequest<Result<PageSectionDto>>
    {
        public UpdatePageSectionDto Data { get; set; } = null!;
    }
}
