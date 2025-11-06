using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostById;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostWithDetailById
{

    public class GetBlogPostWithDetailByIdQueryHandler : IRequestHandler<GetBlogPostWithDetailByIdQuery, Result<BlogPostDetailDto>>
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;

        public GetBlogPostWithDetailByIdQueryHandler(IBlogPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<BlogPostDetailDto>> Handle(GetBlogPostWithDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (post == null)
                return Result<BlogPostDetailDto>.Failure("Post bulunamadı.");

            var dto = _mapper.Map<BlogPostDetailDto>(post);
            return Result<BlogPostDetailDto>.Success(dto);
        }
    }
}
