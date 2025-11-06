using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Queries.GetActiveProfileFirst
{
    public class GetActiveProfileFirstQueryHandler : IRequestHandler<GetActiveProfileFirstQuery, Result<LawyerProfileDto>>
    {
        private readonly ILawyerProfileRepository _repository;
        private readonly IMapper _mapper;

        public GetActiveProfileFirstQueryHandler(ILawyerProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<LawyerProfileDto>> Handle(GetActiveProfileFirstQuery request, CancellationToken cancellationToken)
        {
            var siteSettings = await _repository.GetActiveAsync(cancellationToken);

            if (siteSettings == null)
                return Result<LawyerProfileDto>.Failure("Aktif site ayarları bulunamadı.");

            var dto = _mapper.Map<LawyerProfileDto>(siteSettings);
            return Result<LawyerProfileDto>.Success(dto);

        }
    }

}
