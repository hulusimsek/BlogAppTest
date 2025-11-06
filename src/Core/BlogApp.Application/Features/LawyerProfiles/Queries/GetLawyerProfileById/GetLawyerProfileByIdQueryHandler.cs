using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.Features.LawyerProfiles.Queries.GetAllLawyerProfiles;
using BlogApp.Application.Features.LawyerProfiles.Queries.GetLawyerById;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogApp.Application.Features.LawyerProfiles.Queries.GetLawyerProfileById
{
    public class GetLawyerProfileByIdQueryHandler : IRequestHandler<GetLawyerProfileByIdQuery, Result<LawyerProfileDto>>
    {
        private readonly ILawyerProfileRepository _repository;
        private readonly IMapper _mapper;

        public GetLawyerProfileByIdQueryHandler(ILawyerProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<LawyerProfileDto>> Handle(GetLawyerProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (profile == null)
            {
                return Result<LawyerProfileDto>.Failure("Profil bulunamadı.");
            }
            var dtos = _mapper.Map<LawyerProfileDto>(profile);
            return Result<LawyerProfileDto>.Success(dtos);
        }
    }
}
