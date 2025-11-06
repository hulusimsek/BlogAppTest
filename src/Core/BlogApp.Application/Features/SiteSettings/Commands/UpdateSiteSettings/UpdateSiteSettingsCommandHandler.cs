using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Interfaces.Infrastructure;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.SiteSettings.Commands.UpdateSiteSettings
{
    public class UpdateSiteSettingsCommandHandler : IRequestHandler<UpdateSiteSettingsCommand, Result<SiteSettingsDto>>
    {
        private readonly ISiteSettingsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFaviconService _faviconService;

        public UpdateSiteSettingsCommandHandler(
            ISiteSettingsRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFaviconService faviconService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _faviconService = faviconService;
        }

        public async Task<Result<SiteSettingsDto>> Handle(UpdateSiteSettingsCommand request, CancellationToken cancellationToken)
        {
            var siteSettings = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (siteSettings == null)
            {
                return Result<SiteSettingsDto>.Failure("Site ayarları bulunamadı.");
            }

            _mapper.Map(request.Data, siteSettings);
            // 2️⃣ Favicon işlemi (admin bir dosya yüklediyse)
            if (request.FaviconStream != null && request.FaviconFileName != null)
            {
                var favicons = await _faviconService.GenerateFaviconsAsync(request.FaviconStream, request.FaviconFileName);
                siteSettings.Favicons = favicons;
            }
            siteSettings.SetModifiedDate();

            await _repository.UpdateAsync(siteSettings, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<SiteSettingsDto>(siteSettings);
            return Result<SiteSettingsDto>.Success(dto);
        }
    }

}
