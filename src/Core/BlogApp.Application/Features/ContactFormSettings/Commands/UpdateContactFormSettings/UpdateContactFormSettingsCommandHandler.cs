using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactFormSettings.Commands.UpdateContactFormSettings
{
    public class UpdateContactFormSettingsCommandHandler : IRequestHandler<UpdateContactFormSettingsCommand, Result<ContactFormSettingsDto>>
    {
        private readonly IContactFormSettingsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateContactFormSettingsCommandHandler(
            IContactFormSettingsRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ContactFormSettingsDto>> Handle(UpdateContactFormSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Data.Id, cancellationToken);
            if (entity == null)
                return Result<ContactFormSettingsDto>.Failure("Aktif contact form ayarı bulunamadı.");

            _mapper.Map(request.Data, entity);
            entity.SetModifiedDate();

            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ContactFormSettingsDto>(entity);
            return Result<ContactFormSettingsDto>.Success(dto);
        }
    }
}
