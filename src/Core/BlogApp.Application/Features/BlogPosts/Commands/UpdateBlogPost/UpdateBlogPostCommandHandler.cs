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

namespace BlogApp.Application.Features.BlogPosts.Commands.UpdateBlogPost
{
    public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, Result<BlogPostDto>>
    {
        private readonly IBlogPostRepository _repository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBlogPostCommandHandler(
            IBlogPostRepository postRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = postRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<BlogPostDto>> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var existingPost = await _repository.GetByIdAsync(request.Data.Id, cancellationToken);
            if (existingPost == null)
                return Result<BlogPostDto>.Failure("Post bulunamadı.");

            var currentSlug = existingPost.Slug;
            var currentIsPublished = existingPost.IsPublished;



            _mapper.Map(request.Data, existingPost);
            existingPost.SetModifiedDate();

            if (currentIsPublished == false && existingPost.IsPublished == true)
            {
                existingPost.PublishDate = DateTime.UtcNow;
            }

            // 1 Temel slug üret
            var baseSlug = SlugGenerator.GenerateSlug(request.Data.Title);

            // Eğer slug değişmemişse, herhangi bir değişiklik yapma
            if (baseSlug != currentSlug)
            {
                // Benzersizlik kontrolü yapılacak
                var uniqueSlug = await EnsureUniqueSlugAsync(baseSlug, cancellationToken);
                existingPost.Slug = uniqueSlug;
            }

            // FAQ güncelleme (önce sil, sonra ekle)
            existingPost.Faqs.Clear();
            if (request.Data.BlogFaqItems != null && request.Data.BlogFaqItems.Any())
            {
                foreach (var faq in request.Data.BlogFaqItems)
                    existingPost.Faqs.Add(_mapper.Map<BlogFaqItem>(faq));
            }

            // Tag güncelleme
            existingPost.Tags.Clear();
            if (request.Data.Tags != null && request.Data.Tags.Any())
            {
                foreach (var tagId in request.Data.Tags)
                {
                    existingPost.Tags.Add(new BlogPostTag
                    {
                        BlogPostId = existingPost.Id,
                        TagId = tagId
                    });
                }
            }


            await _repository.UpdateAsync(existingPost, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<BlogPostDto>(existingPost);
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
    }

}
