using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Commands.DeleteLawyerProfileById
{
    public class DeleteLawyerProfileByIdCommandHandler : IRequestHandler<DeleteLawyerProfileByIdCommand, Result>
    {
        private readonly ILawyerProfileRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLawyerProfileByIdCommandHandler(ILawyerProfileRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteLawyerProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var profile = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (profile == null)
            {
                return Result.Failure("Avukat profili bulunamadı.");
            }

            await _repository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Opsiyonel: Unit of Work veya SaveChangesAsync çağrısı gerekiyorsa orada yapılmalı
            return Result.Success();
        }
    }
}
