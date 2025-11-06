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

namespace BlogApp.Application.Features.BlogPosts.Queries.Filter
{
    public class FilterBlogPostsQueryHandler
        : IRequestHandler<FilterBlogPostsQuery, Result<List<BlogPostDto>>>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public FilterBlogPostsQueryHandler(IBlogPostRepository blogPostRepository, IMapper mapper)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<BlogPostDto>>> Handle(FilterBlogPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _blogPostRepository.GetFilteredAsync(
                searchTitle: request.SearchTitle,
                categoryId: request.CategoryId,
                categorySlug: request.CategorySlug,
                tag: request.Tag,
                isPublished: request.IsPublished,
                sortBy: request.SortBy,
                descending: request.Descending,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                isApprovedComments: request.IsApprovedComments,
                cancellationToken: cancellationToken
            );

            var dtoList = _mapper.Map<List<BlogPostDto>>(posts.Posts);
            var result = Result<List<BlogPostDto>>.Success(dtoList);
            result.TotalCount = posts.TotalCount;

            return result;
        }
    }

}
