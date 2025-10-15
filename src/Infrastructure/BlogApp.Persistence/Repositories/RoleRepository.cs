using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using BlogApp.Persistence.Data;
using BlogApp.Persistence.Identity;

namespace BlogApp.Persistence.Repositories;

/// <summary>
/// Role Repository Implementation
/// </summary>
public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RoleRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AppRole?> GetByIdAsync(Guid id)
    {
        var identityRole = await _context.Roles.FindAsync(id);
        if (identityRole == null) return null;

        return _mapper.Map<AppRole>(identityRole);
    }

    public async Task<AppRole?> GetByNameAsync(string name)
    {
        var identityRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name);

        if (identityRole == null) return null;

        return _mapper.Map<AppRole>(identityRole);
    }

    public async Task<IEnumerable<AppRole>> GetAllAsync()
    {
        var roles = await _context.Roles.ToListAsync();
        return _mapper.Map<IEnumerable<AppRole>>(roles);
    }

    public async Task<AppRole> CreateAsync(AppRole role)
    {
        var identityRole = _mapper.Map<IdentityAppRole>(role);
        await _context.Roles.AddAsync(identityRole);
        await _context.SaveChangesAsync();

        return _mapper.Map<AppRole>(identityRole);
    }

    public async Task<AppRole> UpdateAsync(AppRole role)
    {
        var identityRole = await _context.Roles.FindAsync(role.Id);
        if (identityRole == null) throw new Exception("Role not found");

        // Güncellemeleri manuel yapabiliriz
        identityRole.Name = role.Name;
        identityRole.NormalizedName = role.Name.ToUpper();
        identityRole.Description = role.Description;

        _context.Roles.Update(identityRole);
        await _context.SaveChangesAsync();

        return _mapper.Map<AppRole>(identityRole);
    }

    public async Task DeleteAsync(Guid id)
    {
        var identityRole = await _context.Roles.FindAsync(id);
        if (identityRole == null) throw new Exception("Role not found");

        _context.Roles.Remove(identityRole);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Roles.AnyAsync(r => r.Name == name);
    }

    public async Task<IEnumerable<AppRole>> GetUserRolesAsync(Guid userId)
    {
        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AppRole>>(roles);
    }

    public async Task AssignRoleToUserAsync(Guid userId, Guid roleId)
    {
        var userRoleExists = await _context.UserRoles
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (userRoleExists)
            return; // Zaten atanmýþ

        var userRole = new IdentityAppUserRole
        {
            UserId = userId,
            RoleId = roleId,
            AssignedDate = DateTime.UtcNow
        };

        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId)
    {
        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (userRole == null)
            return; // Rol zaten yok

        _context.UserRoles.Remove(userRole);
        await _context.SaveChangesAsync();
    }
}
