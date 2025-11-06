using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Features.BlogPosts.Commands.UpdateBlogPost;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactInfo.Commands.UpdateContactInfo
{
    public class UpdateContactInfoCommandHandler : IRequestHandler<UpdateContactInfoCommand, Result<ContactInfoDto>>
    {
        private readonly IContactInfoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateContactInfoCommandHandler(
            IContactInfoRepository postRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ContactInfoDto>> Handle(UpdateContactInfoCommand request, CancellationToken cancellationToken)
        {
            var contactInfo = await _repository.GetActiveContactInfoAsync(cancellationToken);

            if (contactInfo == null)
            {
                return Result<ContactInfoDto>.Failure("İletişim profili bulunamadı.");
            }

            // Basit alanlar
            _mapper.Map(request.Data, contactInfo);

            // 🔹 Çalışma saatlerini güncelle
            if (request.Data.WorkingHours != null && request.Data.WorkingHours.Any())
            {
                contactInfo.WorkingHours.Clear(); // Eski verileri temizle (veya daha ileri seviye: merge)
                foreach (var whDto in request.Data.WorkingHours)
                {
                    contactInfo.WorkingHours.Add(_mapper.Map<WorkingHour>(whDto));
                }
            }

            contactInfo.Faqs.Clear();
            if (request.Data.Faqs != null && request.Data.Faqs.Any())
            {
                foreach (var faq in request.Data.Faqs)
                    contactInfo.Faqs.Add(_mapper.Map<ContactInfoFaqItem>(faq));
            }

            contactInfo.SetModifiedDate();

            await _repository.UpdateAsync(contactInfo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ContactInfoDto>(contactInfo);
            return Result<ContactInfoDto>.Success(dto);
        }

    }
}
