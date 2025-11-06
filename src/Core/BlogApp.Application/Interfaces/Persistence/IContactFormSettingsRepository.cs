using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface IContactFormSettingsRepository
    {
        Task<ContactFormSettings> CreateAsync(ContactFormSettings request, CancellationToken cancellationToken = default);
        Task<ContactFormSettings?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ContactFormSettings?> GetBySectionKeyAsync(string sectionKey, CancellationToken cancellationToken = default);
        Task UpdateAsync(ContactFormSettings request, CancellationToken cancellationToken = default);
    }
}
