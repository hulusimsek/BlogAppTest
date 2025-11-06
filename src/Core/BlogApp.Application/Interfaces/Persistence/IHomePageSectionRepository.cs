using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface IHomePageSectionRepository
    {
        Task<PageSection?> GetActiveHomePageSectionAsync(string sectionKey, CancellationToken cancellationToken = default);
        Task<PageSection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PageSection> CreateAsync(PageSection homePageSection, CancellationToken cancellationToken = default);
        Task UpdateAsync(PageSection homePageSection, CancellationToken cancellationToken = default);
        Task IncrementViewCountAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
