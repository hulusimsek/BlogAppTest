namespace BlogApp.Domain.Entities;

/// <summary>
/// Domain Entity - Rol
/// </summary>
public class AppRole
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedDate { get; private set; }

    // Navigation properties
    public virtual ICollection<AppUserRole> UserRoles { get; private set; } = new List<AppUserRole>();

    // Private constructor for EF Core
    private AppRole() { }

    public AppRole(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        CreatedDate = DateTime.UtcNow;
    }

    // Pre-defined roles
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Moderator = "Moderator";
    }

    public void UpdateDescription(string description)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}