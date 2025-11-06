using BlogApp.Application.Common;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.DTOs.SiteSettings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Queries.GetActiveProfileFirst
{
    public class GetActiveProfileFirstQuery : IRequest<Result<LawyerProfileDto>>
    {
    }
}
