using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface IBlogPostRepository
    {
        Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool includeUnapprovedComments = false);
        Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default,
                                        bool includeUnapprovedComments = false, bool includeUnPublishedPost = false);
        Task<List<BlogPost>> GetPublishedPostsAsync(int page, int pageSize,
                string orderBy = "PublishDate", bool isDescending = true, CancellationToken cancellationToken = default);
        Task<(List<BlogPost> Posts, int TotalCount)> GetFilteredAsync(string? searchTitle, Guid? categoryId, string? categorySlug, string? tag,
                                                    bool? isPublished, string? sortBy, bool descending, int pageNumber,
                                                    int pageSize, bool? isApprovedComments, CancellationToken cancellationToken);
        Task<List<BlogPost>> GetPostsByCategoryAsync(string categorySlug, int page, int pageSize, Guid? excludePostId, CancellationToken cancellationToken = default,
                                                            bool includeUnPublishedPost = false);
        Task<List<BlogPost>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default,
                                        bool includeUnPublishedPost = false);
        Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<BlogPost> CreateAsync(BlogPost post, CancellationToken cancellationToken = default);
        Task AddTagsToPostAsync(List<BlogPostTag> blogPostTags, CancellationToken cancellationToken = default);
        Task UpdateAsync(BlogPost post, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task IncrementViewCountAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
