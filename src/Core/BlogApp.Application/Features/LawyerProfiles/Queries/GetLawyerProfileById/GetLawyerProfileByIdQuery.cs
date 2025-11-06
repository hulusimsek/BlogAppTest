using BlogApp.Application.Common;
using BlogApp.Application.DTOs.LawyerProfile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Queries.GetLawyerById
{
    public class GetLawyerProfileByIdQuery : IRequest<Result<LawyerProfileDto>>
    {
        public Guid Id { get; set; }
    }
}
