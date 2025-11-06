using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<List<Tag>> GetByIdsAsync(List<Guid> tagIds, CancellationToken cancellationToken = default);
        Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<Tag> CreateAsync(Tag tag, CancellationToken cancellationToken = default);
        Task UpdateAsync(Tag tag, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
