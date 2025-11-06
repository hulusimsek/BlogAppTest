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
    public class ContactFormSettingsRepository : IContactFormSettingsRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactFormSettingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactFormSettings> CreateAsync(ContactFormSettings request, CancellationToken cancellationToken = default)
        {
            await _context.ContactFormSettings.AddAsync(request, cancellationToken);
            return request;
        }

        public async Task<ContactFormSettings?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ContactFormSettings
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<ContactFormSettings?> GetBySectionKeyAsync(string sectionKey, CancellationToken cancellationToken = default)
        {
            return await _context.ContactFormSettings
                .FirstOrDefaultAsync(x => x.SectionKey == sectionKey, cancellationToken);
        }

        public Task UpdateAsync(ContactFormSettings request, CancellationToken cancellationToken = default)
        {
            _context.ContactFormSettings.Update(request);
            return Task.CompletedTask;
        }
    }

}
