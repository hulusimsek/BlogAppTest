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

namespace BlogApp.Application.Features.BlogPosts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQueryHandler : IRequestHandler<GetPostsByCategoryQuery, Result<List<BlogPostDto>>>
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;

        public GetPostsByCategoryQueryHandler(IBlogPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<BlogPostDto>>> Handle(GetPostsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetPostsByCategoryAsync(request.categorySlug, request.Page, request.PageSize, request.ExcludePostId, cancellationToken,
                                                            request.includeUnPublishedPost);
            var dtoList = _mapper.Map<List<BlogPostDto>>(posts);
            return Result<List<BlogPostDto>>.Success(dtoList);
        }
    }

}
