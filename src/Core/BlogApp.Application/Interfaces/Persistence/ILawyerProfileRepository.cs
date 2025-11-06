using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Persistence
{
    public interface ILawyerProfileRepository
    {
        Task<LawyerProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<LawyerProfile>> GetAllActiveAsync(CancellationToken cancellationToken = default);
        Task<LawyerProfile?> GetActiveAsync(CancellationToken cancellationToken = default);
        Task<LawyerProfile> CreateAsync(LawyerProfile profile, CancellationToken cancellationToken = default);
        Task UpdateAsync(LawyerProfile profile, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
