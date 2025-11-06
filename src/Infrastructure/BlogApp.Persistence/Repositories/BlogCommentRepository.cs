using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Repositories
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogComment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.BlogComments
                .Include(c => c.BlogPost)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<BlogComment>> GetCommentsByPostIdAsync(Guid postId, bool onlyApproved = true, CancellationToken cancellationToken = default)
        {
            var query = _context.BlogComments
                .Where(c => c.BlogPostId == postId);

            if (onlyApproved)
                query = query.Where(c => c.IsApproved);

            return await query
                .OrderByDescending(c => c.CommentDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<BlogComment> CreateAsync(BlogComment comment, CancellationToken cancellationToken = default)
        {
            await _context.BlogComments.AddAsync(comment, cancellationToken);
            return comment;
        }

        public async Task ApproveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var comment = await _context.BlogComments.FindAsync(new object[] { id }, cancellationToken);
            if (comment != null)
            {
                comment.IsApproved = true;
                _context.BlogComments.Update(comment);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var comment = await _context.BlogComments.FindAsync(new object[] { id }, cancellationToken);
            if (comment != null)
            {
                _context.BlogComments.Remove(comment);
            }
        }
    }

}
