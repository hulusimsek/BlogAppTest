using BlogApp.Domain.Entities;

namespace BlogApp.Domain.Repositories;

/// <summary>
/// User Repository Interface - Domain katmanında tanımlanır
/// Implementation Infrastructure katmanında yapılır
/// </summary>
public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(Guid id);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser?> GetByUserNameAsync(string userName);
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<AppUser> CreateAsync(AppUser user);
    Task<AppUser> UpdateAsync(AppUser user);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<IEnumerable<AppUser>> GetUsersByRoleAsync(string roleName);
}