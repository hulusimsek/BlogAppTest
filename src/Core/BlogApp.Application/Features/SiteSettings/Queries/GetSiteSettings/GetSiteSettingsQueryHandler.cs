using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings
{
    public class GetSiteSettingsQueryHandler : IRequestHandler<GetSiteSettingsQuery, Result<SiteSettingsDto>>
    {
        private readonly ISiteSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetSiteSettingsQueryHandler(ISiteSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<SiteSettingsDto>> Handle(GetSiteSettingsQuery request, CancellationToken cancellationToken)
        {
            var siteSettings = await _repository.GetActiveSiteSettingsAsync(cancellationToken);

            if (siteSettings == null)
                return Result<SiteSettingsDto>.Failure("Aktif site ayarları bulunamadı.");

            var dto = _mapper.Map<SiteSettingsDto>(siteSettings);
            return Result<SiteSettingsDto>.Success(dto);

        }
    }

}
