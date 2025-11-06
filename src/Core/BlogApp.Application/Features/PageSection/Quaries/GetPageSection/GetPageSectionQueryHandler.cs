using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection
{
    public class GetPageSectionQueryHandler : IRequestHandler<GetPageSectionQuery, Result<PageSectionDto>>
    {
        private readonly IHomePageSectionRepository _repository;
        private readonly IMapper _mapper;

        public GetPageSectionQueryHandler(IHomePageSectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PageSectionDto>> Handle(GetPageSectionQuery request, CancellationToken cancellationToken)
        {
            var siteSettings = await _repository.GetActiveHomePageSectionAsync(request.SectionKey, cancellationToken);

            if (siteSettings == null)
                return Result<PageSectionDto>.Failure("Aktif Ana Sayfa Profili Bulunamadı.");

            var dto = _mapper.Map<PageSectionDto>(siteSettings);
            return Result<PageSectionDto>.Success(dto);

        }
    }
}
