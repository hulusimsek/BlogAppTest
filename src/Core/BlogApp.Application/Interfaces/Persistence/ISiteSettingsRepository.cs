using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface ISiteSettingsRepository
    {
        Task<SiteSettings?> GetActiveSiteSettingsAsync(CancellationToken cancellationToken = default);
        Task<SiteSettings?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SiteSettings> CreateAsync(SiteSettings siteSettings, CancellationToken cancellationToken = default);
        Task UpdateAsync(SiteSettings siteSettings, CancellationToken cancellationToken = default);
    }
}
