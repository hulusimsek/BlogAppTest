using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.DeleteContactRequest
{
    public class DeleteContactRequestCommandHandler : IRequestHandler<DeleteContactRequestCommand, Result<bool>>
    {
        private readonly IContactRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactRequestCommandHandler(IContactRequestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteContactRequestCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
