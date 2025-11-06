using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactFormSettings.Queries.GetContactFormSettings
{
    public class GetContactFormSettingsQueryHandler : IRequestHandler<GetContactFormSettingsQuery, Result<ContactFormSettingsDto>>
    {
        private readonly IContactFormSettingsRepository _repository;
        private readonly IMapper _mapper;

        public GetContactFormSettingsQueryHandler(IContactFormSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ContactFormSettingsDto>> Handle(GetContactFormSettingsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetBySectionKeyAsync(request.SectionKey, cancellationToken);
            if (entity is null)
            {
                return Result<ContactFormSettingsDto>.Failure("Aktif contact form ayarı bulunamadı.");

            }
            return Result<ContactFormSettingsDto>.Success(_mapper.Map<ContactFormSettingsDto>(entity));
        }
    }
}
