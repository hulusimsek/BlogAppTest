namespace BlogApp.Domain.Entities;

/// <summary>
/// Domain Entity - Kullanıcı (Framework bağımsız)
/// Bu sınıf tamamen domain business logic'ini içerir
/// </summary>
public class AppUser
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public bool EmailConfirmed { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? LastLoginDate { get; private set; }
    public bool IsActive { get; private set; }
    
    // Navigation properties
    public virtual ICollection<AppUserRole> UserRoles { get; private set; } = new List<AppUserRole>();

    // Private constructor for EF Core
    private AppUser() { }

    public AppUser(
        string email, 
        string userName, 
        string firstName, 
        string lastName)
    {
        Id = Guid.NewGuid();
        Email = email ?? throw new ArgumentNullException(nameof(email));
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        EmailConfirmed = false;
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
    }

    // Business methods
    public void ConfirmEmail()
    {
        EmailConfirmed = true;
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
    }

    public void SetLastLoginDate()
    {
        LastLoginDate = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public bool HasRole(string roleName)
    {
        return UserRoles.Any(ur => ur.Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
    }

    public void SetUserRoles(List<AppUserRole> roles)
    {
        UserRoles = roles;
    }
}