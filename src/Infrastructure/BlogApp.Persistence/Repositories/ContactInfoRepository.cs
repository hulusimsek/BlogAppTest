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
    public class ContactInfoRepository : IContactInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactInfo?> GetActiveContactInfoAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ContactInfos
                .Include(x => x.WorkingHours)
                .Include(x => x.Faqs)
                .FirstOrDefaultAsync(x => x.IsActive, cancellationToken);
        }

        public async Task<ContactInfo> CreateAsync(ContactInfo contactInfo, CancellationToken cancellationToken = default)
        {
            await _context.ContactInfos.AddAsync(contactInfo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return contactInfo;
        }

        public async Task UpdateAsync(ContactInfo contactInfo, CancellationToken cancellationToken = default)
        {
            _context.ContactInfos.Update(contactInfo);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
