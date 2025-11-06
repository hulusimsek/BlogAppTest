using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactInfo.Queries.Commands
{
    public class GetContactInfoQueryHandler : IRequestHandler<GetContactInfoQuery, Result<ContactInfoDto>>
    {
        private readonly IContactInfoRepository _repository;
        private readonly IMapper _mapper;

        public GetContactInfoQueryHandler(IContactInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ContactInfoDto>> Handle(GetContactInfoQuery request, CancellationToken cancellationToken)
        {
            var contactInfo = await _repository.GetActiveContactInfoAsync(cancellationToken);

            if (contactInfo == null)
                return Result<ContactInfoDto>.Failure("İletişim profili bulunamadı.");

            var dto = _mapper.Map<ContactInfoDto>(contactInfo);
            return Result<ContactInfoDto>.Success(dto);

        }
    }

}
