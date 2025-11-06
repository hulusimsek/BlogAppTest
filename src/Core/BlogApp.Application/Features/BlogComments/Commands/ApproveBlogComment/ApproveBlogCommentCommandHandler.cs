using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogComments.Commands.ApproveBlogComment
{
    public class ApproveBlogCommentCommandHandler : IRequestHandler<ApproveBlogCommentCommand, Result>
    {
        private readonly IBlogCommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveBlogCommentCommandHandler(IBlogCommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ApproveBlogCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(request.CommentId, cancellationToken);
            if (comment == null)
                return Result.Failure("Yorum bulunamadı.");

            comment.IsApproved = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
