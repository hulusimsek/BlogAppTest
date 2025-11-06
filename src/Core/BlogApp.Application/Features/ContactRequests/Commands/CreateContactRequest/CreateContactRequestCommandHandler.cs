using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.CreateContactRequest
{
    public class CreateContactRequestCommandHandler
            : IRequestHandler<CreateContactRequestCommand, Result<ContactRequestDto>>
    {
        private readonly IContactRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateContactRequestCommandHandler(
            IContactRequestRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ContactRequestDto>> Handle(CreateContactRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ContactRequest>(request.Data);
            entity.RequestDate = DateTime.UtcNow;

            await _repository.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ContactRequestDto>(entity);
            return Result<ContactRequestDto>.Success(dto);
        }
    }

}
