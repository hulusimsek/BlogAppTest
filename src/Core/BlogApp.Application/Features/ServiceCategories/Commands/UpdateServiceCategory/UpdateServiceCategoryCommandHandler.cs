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

namespace BlogApp.Application.Features.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommandHandler : IRequestHandler<UpdateServiceCategoryCommand, Result<ServiceCategoryDto>>
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateServiceCategoryCommandHandler(IServiceCategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ServiceCategoryDto>> Handle(UpdateServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Data;

            var existingCategory = await _repository.GetByIdAsync(request.Data.Id, cancellationToken);
            if (existingCategory == null)
            {
                return Result<ServiceCategoryDto>.Failure("Kategori bulunamadı.");
            }

            var currentSlug = existingCategory.Slug;

            // Ana alanlar
            existingCategory.Name = dto.Name;
            existingCategory.IconName = dto.IconName;
            existingCategory.ShortDescription = dto.ShortDescription;
            existingCategory.DisplayOrder = dto.DisplayOrder;
            existingCategory.ShowOnHomePage = dto.ShowOnHomePage;
            existingCategory.IsActive = dto.IsActive;
            existingCategory.MetaTitle = dto.MetaTitle;
            existingCategory.MetaDescription = dto.MetaDescription;
            existingCategory.MetaKeywords = dto.MetaKeywords;

            // Eğer detay varsa
            if (dto.ServiceDetail is not null)
            {
                if (existingCategory.ServiceDetail == null)
                {
                    // Yeni detail oluştur
                    var newDetail = new ServiceDetail
                    {
                        ImageUrl = dto.ServiceDetail.ImageUrl,
                        DetailedDescription = dto.ServiceDetail.DetailedDescription,
                        ServiceCategory = existingCategory
                    };

                    // Alt koleksiyonlar doluysa ekle
                    if (dto.ServiceDetail.ProcessFlow?.Any() == true)
                        newDetail.ProcessFlowSteps = dto.ServiceDetail.ProcessFlow
                            .Select(x => new ProcessFlowStep
                            {
                                StepNumber = x.StepNumber,
                                Title = x.Title,
                                Description = x.Description,
                                IconName = x.IconName,
                                ServiceDetail = newDetail
                            }).ToList();

                    if (dto.ServiceDetail.Faqs?.Any() == true)
                        newDetail.Faqs = dto.ServiceDetail.Faqs
                            .Select(x => new ServiceFaqItem
                            {
                                Question = x.Question,
                                Answer = x.Answer,
                                ServiceDetail = newDetail
                            }).ToList();

                    if (dto.ServiceDetail.Testimonials?.Any() == true)
                        newDetail.Testimonials = dto.ServiceDetail.Testimonials
                            .Select(x => new TestimonialItem
                            {
                                Content = x.Content,
                                AuthorName = x.AuthorName,
                                AuthorLocation = x.AuthorLocation,
                                ServiceDetail = newDetail
                            }).ToList();


                    existingCategory.ServiceDetail = newDetail;
                }
                else
                {
                    // Mevcut detail'i güncelle
                    var detail = existingCategory.ServiceDetail;
                    detail.ImageUrl = dto.ServiceDetail.ImageUrl;
                    detail.DetailedDescription = dto.ServiceDetail.DetailedDescription;

                    SyncProcessFlow(detail, dto.ServiceDetail.ProcessFlow);
                    SyncFaqs(detail, dto.ServiceDetail.Faqs);
                    SyncTestimonials(detail, dto.ServiceDetail.Testimonials);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            
            existingCategory.SetModifiedDate();

            // 1 Temel slug üret
            var baseSlug = SlugGenerator.GenerateSlug(request.Data.Name);

            // Eğer slug değişmemişse, herhangi bir değişiklik yapma
            if (baseSlug != currentSlug)
            {
                // Benzersizlik kontrolü yapılacak
                var uniqueSlug = await EnsureUniqueSlugAsync(baseSlug, cancellationToken);
                existingCategory.Slug = uniqueSlug;
            }

            await _repository.UpdateAsync(existingCategory, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<ServiceCategoryDto>(existingCategory);
            return Result<ServiceCategoryDto>.Success(resultDto);
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

        // 🧩 ProcessFlow senkronizasyonu
        private static void SyncProcessFlow(ServiceDetail detail, List<ProcessFlowStepDto> incoming)
        {
            var existing = detail.ProcessFlowSteps.ToList();
            var incomingIds = incoming.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToHashSet();

            // Silinenleri kaldır
            foreach (var old in existing.Where(x => !incomingIds.Contains(x.Id)).ToList())
                detail.ProcessFlowSteps.Remove(old);

            // Güncelle / ekle
            foreach (var dto in incoming)
            {
                if (dto.Id.HasValue)
                {
                    var existingItem = existing.FirstOrDefault(x => x.Id == dto.Id.Value);
                    if (existingItem != null)
                    {
                        existingItem.StepNumber = dto.StepNumber;
                        existingItem.Title = dto.Title;
                        existingItem.Description = dto.Description;
                        existingItem.IconName = dto.IconName;
                    }
                }
                else
                {
                    detail.ProcessFlowSteps.Add(new ProcessFlowStep
                    {
                        StepNumber = dto.StepNumber,
                        Title = dto.Title,
                        Description = dto.Description,
                        IconName = dto.IconName,
                        ServiceDetail = detail
                    });
                }
            }
        }

        // 🧩 Faq senkronizasyonu
        private static void SyncFaqs(ServiceDetail detail, List<ServiceFaqItemDto> incoming)
        {
            var existing = detail.Faqs.ToList();
            var incomingIds = incoming.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToHashSet();

            foreach (var old in existing.Where(x => !incomingIds.Contains(x.Id)).ToList())
                detail.Faqs.Remove(old);

            foreach (var dto in incoming)
            {
                if (dto.Id.HasValue)
                {
                    var existingItem = existing.FirstOrDefault(x => x.Id == dto.Id.Value);
                    if (existingItem != null)
                    {
                        existingItem.Question = dto.Question;
                        existingItem.Answer = dto.Answer;
                    }
                }
                else
                {
                    detail.Faqs.Add(new ServiceFaqItem
                    {
                        Question = dto.Question,
                        Answer = dto.Answer,
                        ServiceDetail = detail
                    });
                }
            }
        }

        // 🧩 Testimonial senkronizasyonu
        private static void SyncTestimonials(ServiceDetail detail, List<TestimonialItemDto> incoming)
        {
            var existing = detail.Testimonials.ToList();
            var incomingIds = incoming.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToHashSet();

            foreach (var old in existing.Where(x => !incomingIds.Contains(x.Id)).ToList())
                detail.Testimonials.Remove(old);

            foreach (var dto in incoming)
            {
                if (dto.Id.HasValue)
                {
                    var existingItem = existing.FirstOrDefault(x => x.Id == dto.Id.Value);
                    if (existingItem != null)
                    {
                        existingItem.Content = dto.Content;
                        existingItem.AuthorName = dto.AuthorName;
                        existingItem.AuthorLocation = dto.AuthorLocation;
                    }
                }
                else
                {
                    detail.Testimonials.Add(new TestimonialItem
                    {
                        Content = dto.Content,
                        AuthorName = dto.AuthorName,
                        AuthorLocation = dto.AuthorLocation,
                        ServiceDetail = detail
                    });
                }
            }
        }
    }

}
