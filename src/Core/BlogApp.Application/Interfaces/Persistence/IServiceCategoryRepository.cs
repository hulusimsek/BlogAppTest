using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface IServiceCategoryRepository
    {
        Task<ServiceCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceCategory?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<List<ServiceCategory>> GetAllActiveAsync(CancellationToken cancellationToken = default);
        Task<List<TResult>> GetActiveProjectedAsync<TResult>(Expression<Func<ServiceCategory, TResult>> selector,
                                                                CancellationToken cancellationToken = default);
        Task<List<ServiceCategory>> GetHomePageServicesAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<ServiceCategory> CreateAsync(ServiceCategory category, CancellationToken cancellationToken = default);
        Task UpdateAsync(ServiceCategory category, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
