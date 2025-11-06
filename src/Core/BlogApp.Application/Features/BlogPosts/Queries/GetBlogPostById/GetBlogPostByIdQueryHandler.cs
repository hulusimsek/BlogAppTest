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

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostById
{
    public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, Result<BlogPostDto>>
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;

        public GetBlogPostByIdQueryHandler(IBlogPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<BlogPostDto>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetByIdAsync(request.Id, cancellationToken, request.IncludeUnapprovedComments);
            if (post == null)
                return Result<BlogPostDto>.Failure("Post bulunamadı.");

            var dto = _mapper.Map<BlogPostDto>(post);
            return Result<BlogPostDto>.Success(dto);
        }
    }

}
