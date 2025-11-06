using BlogApp.Application.Common;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.SiteSettings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection
{
    public class GetPageSectionQuery : IRequest<Result<PageSectionDto>>
    {
        public string SectionKey { get; set; } = null!;
    }
}
