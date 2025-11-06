using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogComment;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.CreateBlogComment
{
    public class CreateBlogCommentCommandHandler : IRequestHandler<CreateBlogCommentCommand, Result<BlogCommentDto>>
    {
        private readonly IBlogCommentRepository _commentRepository;
        private readonly IBlogPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBlogCommentCommandHandler(
            IBlogCommentRepository commentRepository,
            IBlogPostRepository postRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<BlogCommentDto>> Handle(CreateBlogCommentCommand request, CancellationToken cancellationToken)
        {
            // Blog post var mı kontrol et
            var post = await _postRepository.GetByIdAsync(request.Data.BlogPostId, cancellationToken);
            if (post == null)
                return Result<BlogCommentDto>.Failure("Blog post bulunamadı.");

            var newComment = _mapper.Map<BlogComment>(request.Data);
            newComment.CommentDate = DateTime.UtcNow;
            newComment.IsApproved = false; // varsayılan olarak 

            await _commentRepository.CreateAsync(newComment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<BlogCommentDto>(newComment);
            return Result<BlogCommentDto>.Success(dto);
        }
    }

}
