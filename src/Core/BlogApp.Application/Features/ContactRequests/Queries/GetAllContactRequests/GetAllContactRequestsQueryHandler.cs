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

namespace BlogApp.Application.Features.ContactRequests.Queries.GetAllContactRequests
{
    public class GetAllContactRequestsQueryHandler : IRequestHandler<GetAllContactRequestsQuery, Result<List<ContactRequestDto>>>
    {
        private readonly IContactRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetAllContactRequestsQueryHandler(IContactRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<ContactRequestDto>>> Handle(GetAllContactRequestsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<List<ContactRequestDto>>(entities);
            return Result<List<ContactRequestDto>>.Success(dtos);
        }
    }
}
