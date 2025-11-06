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

namespace BlogApp.Application.Features.BlogPosts.Queries.GetPublishedPosts
{
    public class GetPublishedPostsQueryHandler : IRequestHandler<GetPublishedPostsQuery, Result<List<BlogPostDto>>>
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;

        public GetPublishedPostsQueryHandler(IBlogPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<BlogPostDto>>> Handle(GetPublishedPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetPublishedPostsAsync(page: request.Page, pageSize: request.PageSize, cancellationToken: cancellationToken);
            var dtoList = _mapper.Map<List<BlogPostDto>>(posts);
            return Result<List<BlogPostDto>>.Success(dtoList);
        }
    }
}
