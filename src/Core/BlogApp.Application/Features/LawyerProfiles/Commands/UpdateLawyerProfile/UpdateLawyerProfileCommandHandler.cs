using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.LawyerProfiles.Commands.UpdateLawyerProfile
{
    public class UpdateLawyerProfileCommandHandler : IRequestHandler<UpdateLawyerProfileCommand, Result<LawyerProfileDto>>
    {
        private readonly ILawyerProfileRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLawyerProfileCommandHandler(
            ILawyerProfileRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LawyerProfileDto>> Handle(UpdateLawyerProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _repository.GetByIdAsync(request.Data.Id, cancellationToken);

            if (profile == null)
            {
                return Result<LawyerProfileDto>.Failure("Avukat profili bulunamadı.");
            }

            _mapper.Map(request.Data, profile);
            profile.SetModifiedDate();

            // Update specializations
            profile.Specializations.Clear();
            foreach (var specializationId in request.Data.SpecializationIds)
            {
                profile.Specializations.Add(new LawyerSpecialization
                {
                    LawyerProfileId = profile.Id,
                    ServiceCategoryId = specializationId,
                });
            }

            await _repository.UpdateAsync(profile, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<LawyerProfileDto>(profile);
            return Result<LawyerProfileDto>.Success(dto);
        }
    }

}
