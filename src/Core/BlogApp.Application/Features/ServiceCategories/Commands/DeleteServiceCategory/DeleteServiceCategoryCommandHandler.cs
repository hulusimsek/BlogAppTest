using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommandHandler : IRequestHandler<DeleteServiceCategoryCommand, Result>
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceCategoryCommandHandler(IServiceCategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
