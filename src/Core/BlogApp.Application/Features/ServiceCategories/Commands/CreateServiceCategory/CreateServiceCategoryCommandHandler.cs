using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.Common.Utilities;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommandHandler : IRequestHandler<CreateServiceCategoryCommand, Result<ServiceCategoryDto>>
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateServiceCategoryCommandHandler(
            IServiceCategoryRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ServiceCategoryDto>> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<ServiceCategory>(request.Data);

            //  Slug üretimi
            var baseSlug = SlugGenerator.GenerateSlug(request.Data.Name);
            category.Slug = await EnsureUniqueSlugAsync(baseSlug, cancellationToken);

            //  Eğer detail gönderildiyse oluştur
            if (request.Data.ServiceDetail is not null)
            {
                var sdDto = request.Data.ServiceDetail;
                var serviceDetail = new ServiceDetail
                {
                    ImageUrl = sdDto.ImageUrl,
                    DetailedDescription = sdDto.DetailedDescription,
                    ServiceCategory = category
                };

                // Process steps
                foreach (var pf in sdDto.ProcessFlow)
                    serviceDetail.ProcessFlowSteps.Add(_mapper.Map<ProcessFlowStep>(pf));

                // Faqs
                foreach (var fq in sdDto.Faqs)
                    serviceDetail.Faqs.Add(_mapper.Map<ServiceFaqItem>(fq));

                // Testimonials
                foreach (var t in sdDto.Testimonials)
                    serviceDetail.Testimonials.Add(_mapper.Map<TestimonialItem>(t));

                category.ServiceDetail = serviceDetail;
            }

            await _repository.CreateAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ServiceCategoryDto>(category);
            return Result<ServiceCategoryDto>.Success(dto);
        }


        private async Task<string> EnsureUniqueSlugAsync(string slug, CancellationToken cancellationToken)
        {
            var newSlug = slug;
            int counter = 2;

            while (await _repository.ExistsBySlugAsync(newSlug, cancellationToken))
            {
                newSlug = $"{slug}-{counter++}";
            }

            return newSlug;
        }
    }

}
