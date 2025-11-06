using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogComment;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, Result<List<BlogCommentDto>>>
    {
        private readonly IBlogCommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentsByPostIdQueryHandler(IBlogCommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<BlogCommentDto>>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(request.PostId, request.OnlyApproved, cancellationToken);
            var dtoList = _mapper.Map<List<BlogCommentDto>>(comments);
            return Result<List<BlogCommentDto>>.Success(dtoList);
        }
    }

}
