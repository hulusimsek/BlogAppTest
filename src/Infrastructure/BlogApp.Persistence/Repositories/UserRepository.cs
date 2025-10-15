using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using BlogApp.Persistence.Data;
using BlogApp.Persistence.Identity;

namespace BlogApp.Persistence.Repositories;

/// <summary>
/// User Repository Implementation using Mapping Approach
/// IdentityAppUser (Infrastructure) ↔ AppUser (Domain) mapping
/// Enterprise pattern: Domain stays pure, Infrastructure handles persistence
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityAppUser> _userManager;
    private readonly IMapper _mapper;

    public UserRepository(
        ApplicationDbContext context, 
        UserManager<IdentityAppUser> userManager,
        IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<AppUser?> GetByIdAsync(Guid id)
    {
        var identityUser = await _userManager.FindByIdAsync(id.ToString());
        if (identityUser == null) return null;
        
        var domainUser = _mapper.Map<AppUser>(identityUser);
        
        // Load user roles separately (Domain navigation property)
        var roles = await _userManager.GetRolesAsync(identityUser);
        // Note: UserRoles navigation property will be handled by upper layers if needed
        
        return domainUser;
    }

    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        if (identityUser == null) return null;
        
        return _mapper.Map<AppUser>(identityUser);
    }

    public async Task<AppUser?> GetByUserNameAsync(string userName)
    {
        var identityUser = await _userManager.FindByNameAsync(userName);
        if (identityUser == null) return null;
        
        return _mapper.Map<AppUser>(identityUser);
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        var identityUsers = await _userManager.Users.ToListAsync();
        return _mapper.Map<IEnumerable<AppUser>>(identityUsers);
    }

    public async Task<AppUser> CreateAsync(AppUser user)
    {
        // Map domain user to IdentityAppUser
        var identityUser = _mapper.Map<IdentityAppUser>(user);
        
        // Create via UserManager (handles password hashing, etc.)
        var result = await _userManager.CreateAsync(identityUser);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed: {errors}");
        }
        
        // Return mapped domain user
        return _mapper.Map<AppUser>(identityUser);
    }

    public async Task<AppUser> UpdateAsync(AppUser user)
    {
        // Find existing IdentityAppUser
        var identityUser = await _userManager.FindByIdAsync(user.Id.ToString());
        if (identityUser == null)
        {
            throw new InvalidOperationException($"User with ID {user.Id} not found");
        }
        
        // Update properties from domain user
        identityUser.FirstName = user.FirstName;
        identityUser.LastName = user.LastName;
        identityUser.Email = user.Email;
        identityUser.UserName = user.UserName;
        identityUser.EmailConfirmed = user.EmailConfirmed;
        identityUser.LastLoginDate = user.LastLoginDate;
        identityUser.IsActive = user.IsActive;
        identityUser.NormalizedEmail = user.Email.ToUpper();
        identityUser.NormalizedUserName = user.UserName.ToUpper();
        
        var result = await _userManager.UpdateAsync(identityUser);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User update failed: {errors}");
        }
        
        return _mapper.Map<AppUser>(identityUser);
    }

    public async Task DeleteAsync(Guid id)
    {
        var identityUser = await _userManager.FindByIdAsync(id.ToString());
        if (identityUser != null)
        {
            var result = await _userManager.DeleteAsync(identityUser);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User deletion failed: {errors}");
            }
        }
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        return identityUser != null;
    }

    public async Task<bool> ExistsByUserNameAsync(string userName)
    {
        var identityUser = await _userManager.FindByNameAsync(userName);
        return identityUser != null;
    }

    public async Task<IEnumerable<AppRole>> GetUserRolesAsync(Guid userId)
    {
        var identityUser = await _userManager.FindByIdAsync(userId.ToString());
        if (identityUser == null) return new List<AppRole>();
        
        var roleNames = await _userManager.GetRolesAsync(identityUser);
        
        // Convert role names to domain Role entities
        var roles = new List<AppRole>();
        foreach (var roleName in roleNames)
        {
            var domainRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName);
            if (domainRole != null)
            {
                roles.Add(_mapper.Map<AppRole>(domainRole));
            }
        }
        
        return roles;
    }

    public async Task AssignRoleToUserAsync(Guid userId, Guid roleId)
    {
        var identityUser = await _userManager.FindByIdAsync(userId.ToString());
        if (identityUser == null) return;
        
        var role = await _context.Roles.FindAsync(roleId);
        if (role == null) return;
        
        await _userManager.AddToRoleAsync(identityUser, role.Name ?? "");
    }

    public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId)
    {
        var identityUser = await _userManager.FindByIdAsync(userId.ToString());
        if (identityUser == null) return;
        
        var role = await _context.Roles.FindAsync(roleId);
        if (role == null) return;
        
        await _userManager.RemoveFromRoleAsync(identityUser, role.Name!);
    }

    public async Task<IEnumerable<AppUser>> GetUsersByRoleAsync(string roleName)
    {
        var identityUsersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        return _mapper.Map<IEnumerable<AppUser>>(identityUsersInRole);
    }
}