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
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool includeUnapprovedComments = false)
        {

            IQueryable<BlogPost> query = _context.BlogPosts
                .Include(x => x.ServiceCategory)
                .Include(x => x.Faqs)
                .Include(x => x.Tags).ThenInclude(t => t.Tag);

            if (includeUnapprovedComments)
            {
                query = query.Include(x => x.Comments); // tüm yorumlar dahil
            }
            else
            {
                // EF Core 5+ ile collection içinde filtreleme
                query = query.Include(x => x.Comments.Where(c => c.IsApproved));
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            //return await _context.BlogPosts
            //    .Include(x => x.ServiceCategory)
            //    .Include(x => x.Tags)
            //        .ThenInclude(t => t.Tag)
            //    .Include(x => x.Comments.Where(c => c.IsApproved))
            //    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default,
                                                    bool includeUnapprovedComments = false, bool includeUnPublishedPost = false)
        {
            IQueryable<BlogPost> query = _context.BlogPosts
                .Include(x => x.ServiceCategory)
                .Include(x => x.Faqs)
                .Include(x => x.Tags).ThenInclude(t => t.Tag);


            if (includeUnapprovedComments)
            {
                query = query.Include(x => x.Comments); // tüm yorumlar dahil
            }
            else
            {
                // EF Core 5+ ile collection içinde filtreleme
                query = query.Include(x => x.Comments.Where(c => c.IsApproved));
            }

            if (includeUnPublishedPost)
            {
                return await query.FirstOrDefaultAsync(x => x.Slug == slug);
            }
            else
            {
                return await query.FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished, cancellationToken);
            }

            //return await _context.BlogPosts
            //    .Include(x => x.ServiceCategory)
            //    .Include(x => x.Tags)
            //        .ThenInclude(t => t.Tag)
            //    .Include(x => x.Comments.Where(c => c.IsApproved))
            //    .FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished, cancellationToken);
        }

        public async Task<List<BlogPost>> GetPublishedPostsAsync(int page, int pageSize,
                string orderBy = "PublishDate", bool isDescending = true, CancellationToken cancellationToken = default)
        {
            // Başlangıç sorgusu
            var query = _context.BlogPosts
                .Include(x => x.ServiceCategory)
                .Include(x => x.Tags)
                    .ThenInclude(t => t.Tag)
                .Where(x => x.IsPublished && x.PublishDate <= DateTime.UtcNow);

            // Dinamik sıralama
            query = orderBy.ToLower() switch
            {
                "viewcount" => isDescending ? query.OrderByDescending(x => x.ViewCount) : query.OrderBy(x => x.ViewCount),
                "publishdate" => isDescending ? query.OrderByDescending(x => x.PublishDate) : query.OrderBy(x => x.PublishDate),
                "title" => isDescending ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
                _ => isDescending ? query.OrderByDescending(x => x.PublishDate) : query.OrderBy(x => x.PublishDate), // Varsayılan sıralama PublishDate
            };

            // Sayfalama
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<(List<BlogPost> Posts, int TotalCount)> GetFilteredAsync(string? searchTitle, Guid? categoryId, string? categorySlug, string? tag,
            bool? isPublished, string? sortBy, bool descending, int pageNumber, int pageSize, bool? isApprovedComments, CancellationToken cancellationToken)
        {
            var query = _context.BlogPosts
                .Include(p => p.Comments)
                .Include(p => p.ServiceCategory)
                .Include(p => p.Tags)
                    .ThenInclude(bt => bt.Tag)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTitle))
                query = query.Where(p => p.Title.Contains(searchTitle));

            if (categoryId.HasValue)
                query = query.Where(p => p.ServiceCategoryId == categoryId.Value);

            else if (!string.IsNullOrWhiteSpace(categorySlug))
                query = query.Where(p => p.ServiceCategory != null && p.ServiceCategory.Slug == categorySlug);

            if (!string.IsNullOrWhiteSpace(tag))
                query = query.Where(p => p.Tags.Any(pt => pt.Tag.Slug == tag));

            if (isPublished.HasValue)
                query = query.Where(p => p.IsPublished == isPublished.Value);

            var totalCount = await query.CountAsync(cancellationToken);

            // Sıralama
            query = (sortBy?.ToLower()) switch
            {
                "viewcount" => descending ? query.OrderByDescending(p => p.ViewCount) : query.OrderBy(p => p.ViewCount),
                "title" => descending ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                _ => descending ? query.OrderByDescending(p => p.PublishDate) : query.OrderBy(p => p.PublishDate)
            };

            // Sayfalama
            query = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize);

            var list = await query.ToListAsync(cancellationToken);

            // Onaylı yorum filtresi (in-memory)
            if (isApprovedComments.HasValue)
            {
                foreach (var post in list)
                {
                    post.Comments = post.Comments
                        .Where(c => c.IsApproved == isApprovedComments.Value)
                        .ToList();
                }
            }

            return (list, totalCount);
        }


        public async Task<List<BlogPost>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default,
                                                bool includeUnPublishedPost = false)
        {
            IQueryable<BlogPost> query = _context.BlogPosts
                .Include(x => x.ServiceCategory)
                .Include(x => x.Tags).ThenInclude(t => t.Tag);

            if (!includeUnPublishedPost)
            {
                query = query.Where(x => x.IsPublished);
            }

            return await query
                .OrderByDescending(x => x.PublishDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }


        public async Task<List<BlogPost>> GetPostsByCategoryAsync(string categorySlug, int page, int pageSize, Guid? excludePostId, CancellationToken cancellationToken = default,
                                                            bool includeUnPublishedPost = false)
        {
            IQueryable<BlogPost> query = _context.BlogPosts
                .Include(x => x.ServiceCategory)
                .Include(x => x.Tags).ThenInclude(t => t.Tag);

            // Servis ID'sine göre filtreleme
            query = query.Where(x => x.ServiceCategory != null && x.ServiceCategory.Slug == categorySlug);

            // Mevcut yazıyı hariç tut (ilgili yazılar için)
            if (excludePostId.HasValue)
            {
                query = query.Where(b => b.Id != excludePostId.Value);
            }

            if (!includeUnPublishedPost)
            {
                query = query.Where(x => x.IsPublished);
            }

            return await query.OrderByDescending(x => x.PublishDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.BlogPosts.AnyAsync(x => x.Slug == slug, cancellationToken);
        }
        public async Task<BlogPost> CreateAsync(BlogPost post, CancellationToken cancellationToken = default)
        {
            await _context.BlogPosts.AddAsync(post, cancellationToken);
            return post;
        }

        public async Task AddTagsToPostAsync(List<BlogPostTag> blogPostTags, CancellationToken cancellationToken = default)
        {
            await _context.BlogPostTags.AddRangeAsync(blogPostTags, cancellationToken);
        }

        public Task UpdateAsync(BlogPost post, CancellationToken cancellationToken = default)
        {
            _context.BlogPosts.Update(post);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var post = await _context.BlogPosts.FindAsync(new object[] { id }, cancellationToken);
            if (post != null)
            {
                _context.BlogPosts.Remove(post);
            }
        }

        public async Task IncrementViewCountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var post = await _context.BlogPosts.FindAsync(new object[] { id }, cancellationToken);
            if (post != null)
            {
                post.ViewCount++;
            }
        }
    }

}
