namespace BlogApp.Domain.Entities;

/// <summary>
/// Domain Entity - Kullanıcı-Rol ilişkisi
/// </summary>
public class AppUserRole
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public DateTime AssignedDate { get; private set; }

    // Navigation properties
    public AppUser User { get; private set; } = null!;
    public AppRole Role { get; private set; } = null!;

    // Private constructor for EF Core
    private AppUserRole() { }

    public AppUserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
        AssignedDate = DateTime.UtcNow;
    }
}