using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface IContactInfoRepository
    {
        Task<ContactInfo?> GetActiveContactInfoAsync(CancellationToken cancellationToken = default);
        Task<ContactInfo> CreateAsync(ContactInfo contactInfo, CancellationToken cancellationToken = default);
        Task UpdateAsync(ContactInfo contactInfo, CancellationToken cancellationToken = default);
    }
}
