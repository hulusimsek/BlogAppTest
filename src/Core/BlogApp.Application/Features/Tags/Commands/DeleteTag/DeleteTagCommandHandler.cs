using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Result>
    {
        private readonly ITagRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (tag == null)
                return Result.Failure("Tag buluanamadı.");

            await _repository.DeleteAsync(tag.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
