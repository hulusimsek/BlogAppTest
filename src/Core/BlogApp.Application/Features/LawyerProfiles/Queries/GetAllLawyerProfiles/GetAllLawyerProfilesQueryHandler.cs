using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Queries.GetAllLawyerProfiles
{
    public class GetAllLawyerProfilesQueryHandler : IRequestHandler<GetAllLawyerProfilesQuery, Result<List<LawyerProfileDto>>>
    {
        private readonly ILawyerProfileRepository _repository;
        private readonly IMapper _mapper;

        public GetAllLawyerProfilesQueryHandler(ILawyerProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<LawyerProfileDto>>> Handle(GetAllLawyerProfilesQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _repository.GetAllActiveAsync(cancellationToken);
            var dtos = _mapper.Map<List<LawyerProfileDto>>(profiles);
            return Result<List<LawyerProfileDto>>.Success(dtos);
        }
    }

}
