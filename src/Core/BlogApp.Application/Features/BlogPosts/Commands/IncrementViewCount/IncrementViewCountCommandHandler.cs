using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.IncrementViewCount
{
    public class IncrementViewCountCommandHandler : IRequestHandler<IncrementViewCountCommand, Result>
    {
        private readonly IBlogPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncrementViewCountCommandHandler(IBlogPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(IncrementViewCountCommand request, CancellationToken cancellationToken)
        {
            if(!request.Id.HasValue)
            {
                return Result.Failure();
            }
            var id = request.Id.Value;
            await _postRepository.IncrementViewCountAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
