using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.DeleteBlogComment
{
    public class DeleteBlogCommentCommandHandler : IRequestHandler<DeleteBlogCommentCommand, Result>
    {
        private readonly IBlogCommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogCommentCommandHandler(IBlogCommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteBlogCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(request.CommentId, cancellationToken);
            if (comment == null)
                return Result.Failure("Yorum bulunamadı.");

            await _commentRepository.DeleteAsync(request.CommentId, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
