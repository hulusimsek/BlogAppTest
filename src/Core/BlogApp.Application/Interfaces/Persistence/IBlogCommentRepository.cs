using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface IBlogCommentRepository
    {
        Task<BlogComment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<BlogComment>> GetCommentsByPostIdAsync(Guid postId, bool onlyApproved = true, CancellationToken cancellationToken = default);
        Task<BlogComment> CreateAsync(BlogComment comment, CancellationToken cancellationToken = default);
        Task ApproveAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
