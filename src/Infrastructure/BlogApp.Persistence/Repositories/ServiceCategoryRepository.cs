using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Repositories
{
    public class ServiceCategoryRepository : IServiceCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ServiceCategories
                .Include(c => c.ServiceDetail)
                    .ThenInclude(d => d.ProcessFlowSteps)
                .Include(c => c.ServiceDetail)
                    .ThenInclude(d => d.Faqs)
                .Include(c => c.ServiceDetail)
                    .ThenInclude(d => d.Testimonials)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<ServiceCategory?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.ServiceCategories
                .Include(x => x.ServiceDetail)
                    .ThenInclude(x => x.Testimonials)
                .Include(x => x.ServiceDetail)
                    .ThenInclude(x => x.ProcessFlowSteps)
                .Include(x => x.ServiceDetail)
                    .ThenInclude(x => x.Faqs)
                .FirstOrDefaultAsync(x => x.Slug == slug && x.IsActive, cancellationToken);
        }

        public async Task<List<ServiceCategory>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ServiceCategories
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<TResult>> GetActiveProjectedAsync<TResult>(Expression<Func<ServiceCategory, TResult>> selector,
                                                                        CancellationToken cancellationToken = default)
        {
            return await _context.ServiceCategories
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ServiceCategory>> GetHomePageServicesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ServiceCategories
                .Where(x => x.IsActive && x.ShowOnHomePage)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.ServiceCategories.AnyAsync(x => x.Slug == slug, cancellationToken);
        }


        public async Task<ServiceCategory> CreateAsync(ServiceCategory category, CancellationToken cancellationToken = default)
        {
            await _context.ServiceCategories.AddAsync(category, cancellationToken);
            return category;
        }

        public Task UpdateAsync(ServiceCategory category, CancellationToken cancellationToken = default)
        {
            _context.ServiceCategories.Update(category);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _context.ServiceCategories.FindAsync(new object[] { id }, cancellationToken);
            if (category != null)
            {
                _context.ServiceCategories.Remove(category);
            }
        }
    }

}
