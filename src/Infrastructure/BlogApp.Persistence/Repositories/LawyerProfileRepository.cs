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
    public class LawyerProfileRepository : ILawyerProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public LawyerProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LawyerProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.LawyerProfiles
                .Include(x => x.Specializations)
                    .ThenInclude(s => s.ServiceCategory)
                .Include(x => x.CareerHistory)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<LawyerProfile>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.LawyerProfiles
                .Include(x => x.Specializations)
                    .ThenInclude(s => s.ServiceCategory)
                .Include(x => x.CareerHistory)
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<LawyerProfile?> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.LawyerProfiles
                .Include(x => x.Specializations)
                    .ThenInclude(s => s.ServiceCategory)
                .Include(x => x.CareerHistory)
                .FirstOrDefaultAsync(x => x.IsActive, cancellationToken);
        }

        public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.ServiceCategories.AnyAsync(x => x.Slug == slug, cancellationToken);
        }

        public async Task<LawyerProfile> CreateAsync(LawyerProfile profile, CancellationToken cancellationToken = default)
        {
            await _context.LawyerProfiles.AddAsync(profile, cancellationToken);
            return profile;
        }

        public Task UpdateAsync(LawyerProfile profile, CancellationToken cancellationToken = default)
        {
            _context.LawyerProfiles.Update(profile);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var profile = await _context.LawyerProfiles.FindAsync(new object[] { id }, cancellationToken);
            if (profile != null)
            {
                _context.LawyerProfiles.Remove(profile);
            }
        }
    }

}
