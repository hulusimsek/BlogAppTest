using BlogApp.Application.Common;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Commands.DeleteBlogPost
{
    public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, Result>
    {
        private readonly IBlogPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogPostCommandHandler(IBlogPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
            if (post == null)
                return Result.Failure("Post bulunamadı.");

            await _postRepository.DeleteAsync(post.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
