using BlogApp.Domain.Entities;

namespace BlogApp.Domain.Repositories;

/// <summary>
/// Role Repository Interface
/// </summary>
public interface IRoleRepository
{
    Task<AppRole?> GetByIdAsync(Guid id);
    Task<AppRole?> GetByNameAsync(string name);
    Task<IEnumerable<AppRole>> GetAllAsync();
    Task<AppRole> CreateAsync(AppRole role);
    Task<AppRole> UpdateAsync(AppRole role);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
    Task<IEnumerable<AppRole>> GetUserRolesAsync(Guid userId);
    Task AssignRoleToUserAsync(Guid userId, Guid roleId);
    Task RemoveRoleFromUserAsync(Guid userId, Guid roleId);
}