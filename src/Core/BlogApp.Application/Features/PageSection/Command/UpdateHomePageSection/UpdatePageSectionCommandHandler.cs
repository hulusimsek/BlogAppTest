using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.Features.LawyerProfiles.Commands.UpdateLawyerProfile;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.PageSection.Command.UpdateHomePageSection
{
    public class UpdatePageSectionCommandHandler : IRequestHandler<UpdatePageSectionCommand, Result<PageSectionDto>>
    {
        private readonly IHomePageSectionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePageSectionCommandHandler(
            IHomePageSectionRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PageSectionDto>> Handle(UpdatePageSectionCommand request, CancellationToken cancellationToken)
        {
            var profile = await _repository.GetByIdAsync(request.Data.Id, cancellationToken);

            if (profile == null)
            {
                return Result<PageSectionDto>.Failure("Ana sayfa orofili bulunamadı.");
            }

            _mapper.Map(request.Data, profile);
            profile.SetModifiedDate();


            await _repository.UpdateAsync(profile, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<PageSectionDto>(profile);
            return Result<PageSectionDto>.Success(dto);
        }
    }
}
