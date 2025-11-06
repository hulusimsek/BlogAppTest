using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.MarkAsRead
{
    public class MarkAsUnreadCommandHandler : IRequestHandler<MarkAsUnreadCommand, Result<bool>>
    {
        private readonly IContactRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAsUnreadCommandHandler(IContactRequestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(MarkAsUnreadCommand request, CancellationToken cancellationToken)
        {
            await _repository.MarkAsReadAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }

}
