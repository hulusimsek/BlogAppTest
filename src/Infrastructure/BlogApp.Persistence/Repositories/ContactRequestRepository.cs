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
    public class ContactRequestRepository : IContactRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactRequest> CreateAsync(ContactRequest request, CancellationToken cancellationToken = default)
        {
            request.RequestDate = DateTime.UtcNow;
            await _context.ContactRequests.AddAsync(request, cancellationToken);
            return request;
        }

        public async Task<ContactRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ContactRequests
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<ContactRequest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ContactRequests
                .OrderByDescending(x => x.RequestDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ContactRequest>> GetUnreadAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ContactRequests
                .Where(x => !x.IsRead)
                .OrderByDescending(x => x.RequestDate)
                .ToListAsync(cancellationToken);
        }

        public async Task MarkAsReadAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var request = await _context.ContactRequests.FindAsync(new object[] { id }, cancellationToken);
            if (request != null)
            {
                request.IsRead = true;
            }
        }

        public async Task MarkAsUnreadAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var request = await _context.ContactRequests.FindAsync(new object[] { id }, cancellationToken);
            if (request != null)
            {
                request.IsRead = false;
            }
        }

        public async Task MarkAsRepliedAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var request = await _context.ContactRequests.FindAsync(new object[] { id }, cancellationToken);
            if (request != null)
            {
                request.IsReplied = true;
                request.ReplyDate = DateTime.UtcNow;
            }
        }

        public async Task MarkAsUnrepliedAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var request = await _context.ContactRequests.FindAsync(new object[] { id }, cancellationToken);
            if (request != null)
            {
                request.IsReplied = false;
                request.ReplyDate = null;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var request = await _context.ContactRequests.FindAsync(new object[] { id }, cancellationToken);
            if (request != null)
            {
                _context.ContactRequests.Remove(request);
            }
        }

    }

}
