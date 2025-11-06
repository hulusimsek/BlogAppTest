using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Commands.MarkAsUnreplied
{
    public class MarkAsUnrepliedCommandHandler : IRequestHandler<MarkAsUnrepliedCommand, Result<bool>>
    {
        private readonly IContactRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAsUnrepliedCommandHandler(IContactRequestRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(MarkAsUnrepliedCommand request, CancellationToken cancellationToken)
        {
            await _repository.MarkAsUnrepliedAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }

}
