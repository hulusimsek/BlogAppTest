using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.PageSection.Commands.IncrementHomeViewCount
{
    public class IncrementViewCountCommandHandler : IRequestHandler<IncrementViewCountCommand, Result>
    {
        private readonly IHomePageSectionRepository _homeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncrementViewCountCommandHandler(IHomePageSectionRepository postRepository, IUnitOfWork unitOfWork)
        {
            _homeRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(IncrementViewCountCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue)
            {
                return Result.Failure("ID cannot be null");
            }

            var nonNullableId = request.Id.Value;

            await _homeRepository.IncrementViewCountAsync(nonNullableId, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
