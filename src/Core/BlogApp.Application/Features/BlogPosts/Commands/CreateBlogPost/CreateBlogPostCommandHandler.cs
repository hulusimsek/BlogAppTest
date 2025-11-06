using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.Common.Utilities;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.CreateBlogPost
{
    public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, Result<BlogPostDto>>
    {
        private readonly IBlogPostRepository _repository;
        private readonly IServiceCategoryRepository _serviceCategoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBlogPostCommandHandler(
            IBlogPostRepository repository,
            IUnitOfWork unitOfWork,
            IServiceCategoryRepository serviceCategoryRepository,
            ITagRepository tagRepository,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _serviceCategoryRepository = serviceCategoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<Result<BlogPostDto>> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var serviceCategory = await _serviceCategoryRepository.GetByIdAsync(request.Data.ServiceCategoryId, cancellationToken);
            if (serviceCategory == null)
            {
                return Result<BlogPostDto>.Failure("Geçersiz kategori.");
            }


            var baseSlug = SlugGenerator.GenerateSlug(request.Data.Title);
            var uniqueSlug = await EnsureUniqueSlugAsync(baseSlug, cancellationToken);

            var blogPost = _mapper.Map<BlogPost>(request.Data);
            blogPost.Slug = uniqueSlug;
            blogPost.ViewCount = 0;  // İlk başta 0 görüntülenme
            blogPost.IsPublished = request.Data.IsPublished;

            if (request.Data.Tags != null && request.Data.Tags.Any())
            {
                blogPost.Tags = request.Data.Tags.Select(tagId => new BlogPostTag
                {
                    TagId = tagId,
                    BlogPostId = blogPost.Id
                }).ToList();
            }

            if (blogPost.IsPublished)
            {
                blogPost.PublishDate = DateTime.UtcNow;  // Yayın tarihini otomatik olarak set et
            }

            // 4. Blog postu veritabanına kaydet
            await _repository.CreateAsync(blogPost, cancellationToken);

            // 5. Etiketleri ekle
            await AddTagsToPostAsync(blogPost.Id, request.Data.Tags, cancellationToken);

            // 6. İşlemi kaydet
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 7. DTO'ya dönüştür
            var dto = _mapper.Map<BlogPostDto>(blogPost);
            return Result<BlogPostDto>.Success(dto);
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

        private async Task AddTagsToPostAsync(Guid postId, List<Guid> tagIds, CancellationToken cancellationToken)
        {
            var tags = await _tagRepository.GetByIdsAsync(tagIds, cancellationToken);
            var blogPostTags = tags.Select(tag => new BlogPostTag
            {
                BlogPostId = postId,
                TagId = tag.Id
            }).ToList();

            // Blog post etiketlerini ekle
            await _repository.AddTagsToPostAsync(blogPostTags, cancellationToken);
        }

    }

}
