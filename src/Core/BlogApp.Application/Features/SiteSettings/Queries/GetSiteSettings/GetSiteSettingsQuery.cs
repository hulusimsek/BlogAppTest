using BlogApp.Application.Common;
using BlogApp.Application.DTOs.SiteSettings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings
{
    public class GetSiteSettingsQuery : IRequest<Result<SiteSettingsDto>>
    {
    }
}
