using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface IContactRequestRepository
    {
        Task<ContactRequest> CreateAsync(ContactRequest request, CancellationToken cancellationToken = default);
        Task<ContactRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<ContactRequest>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<ContactRequest>> GetUnreadAsync(CancellationToken cancellationToken = default);
        Task MarkAsUnreadAsync(Guid id, CancellationToken cancellationToken = default);
        Task MarkAsReadAsync(Guid id, CancellationToken cancellationToken = default);
        Task MarkAsUnrepliedAsync(Guid id, CancellationToken cancellationToken = default);
        Task MarkAsRepliedAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
