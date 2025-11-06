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
    public class SiteSettingsRepository : ISiteSettingsRepository
    {
        private readonly ApplicationDbContext _context;

        public SiteSettingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SiteSettings?> GetActiveSiteSettingsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SiteSettings
                .FirstOrDefaultAsync(x => x.IsActive, cancellationToken);
        }

        public async Task<SiteSettings?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SiteSettings
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<SiteSettings> CreateAsync(SiteSettings siteSettings, CancellationToken cancellationToken = default)
        {
            await _context.SiteSettings.AddAsync(siteSettings, cancellationToken);
            return siteSettings;
        }

        public Task UpdateAsync(SiteSettings siteSettings, CancellationToken cancellationToken = default)
        {
            _context.SiteSettings.Update(siteSettings);
            return Task.CompletedTask;
        }
    }

}
