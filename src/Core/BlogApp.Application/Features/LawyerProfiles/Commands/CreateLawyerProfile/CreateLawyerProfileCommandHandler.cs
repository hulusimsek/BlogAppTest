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

namespace BlogApp.Application.Features.LawyerProfiles.Commands.CreateLawyerProfile
{
    public class CreateLawyerProfileCommandHandler : IRequestHandler<CreateLawyerProfileCommand, Result<LawyerProfileDto>>
    {
        private readonly ILawyerProfileRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLawyerProfileCommandHandler(
            ILawyerProfileRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LawyerProfileDto>> Handle(CreateLawyerProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _mapper.Map<LawyerProfile>(request.Data);

            // Add specializations
            foreach (var specializationId in request.Data.SpecializationIds)
            {
                profile.Specializations.Add(new LawyerSpecialization
                {
                    LawyerProfileId = profile.Id,
                    ServiceCategoryId = specializationId,
                });
            }

            await _repository.CreateAsync(profile, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<LawyerProfileDto>(profile);
            return Result<LawyerProfileDto>.Success(dto);
        }
    }

}
