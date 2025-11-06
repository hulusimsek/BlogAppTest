using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostBySlug
{
    public class GetBlogPostBySlugQueryHandler : IRequestHandler<GetBlogPostBySlugQuery, Result<BlogPostDetailDto>>
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;

        public GetBlogPostBySlugQueryHandler(IBlogPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<BlogPostDetailDto>> Handle(GetBlogPostBySlugQuery request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetBySlugAsync(request.Slug, cancellationToken, request.IncludeUnapprovedComments, request.includeUnPublishedPost);
            if (post == null)
                return Result<BlogPostDetailDto>.Failure("Post bulunamadı.");

            var dto = _mapper.Map<BlogPostDetailDto>(post);
            return Result<BlogPostDetailDto>.Success(dto);
        }
    }
}
