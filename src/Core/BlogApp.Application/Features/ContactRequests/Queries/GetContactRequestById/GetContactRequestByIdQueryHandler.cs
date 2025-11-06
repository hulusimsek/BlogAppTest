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

namespace BlogApp.Application.Features.ContactRequests.Queries.GetContactRequestById
{
    public class GetContactRequestByIdQueryHandler : IRequestHandler<GetContactRequestByIdQuery, Result<ContactRequestDto>>
    {
        private readonly IContactRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetContactRequestByIdQueryHandler(IContactRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ContactRequestDto>> Handle(GetContactRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return Result<ContactRequestDto>.Failure("İletişim isteği bulunamadı.");

            var dto = _mapper.Map<ContactRequestDto>(entity);
            return Result<ContactRequestDto>.Success(dto);
        }
    }

}
