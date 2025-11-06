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
    internal class HomePageSectionRepository : IHomePageSectionRepository
    {
        private readonly ApplicationDbContext _context;

        public HomePageSectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageSection?> GetActiveHomePageSectionAsync(string sectionKey, CancellationToken cancellationToken = default)
        {
            return await _context.PageSection
                .FirstOrDefaultAsync(x => x.IsActive && x.SectionKey == sectionKey, cancellationToken);
        }

        public async Task<PageSection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.PageSection
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<PageSection> CreateAsync(PageSection PageSection, CancellationToken cancellationToken = default)
        {
            await _context.PageSection.AddAsync(PageSection, cancellationToken);
            return PageSection;
        }

        public Task UpdateAsync(PageSection PageSection, CancellationToken cancellationToken = default)
        {
            _context.PageSection.Update(PageSection);
            return Task.CompletedTask;
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
