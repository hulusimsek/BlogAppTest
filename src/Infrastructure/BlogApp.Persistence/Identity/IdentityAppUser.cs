using Microsoft.AspNetCore.Identity;

namespace BlogApp.Persistence.Identity;

/// <summary>
/// Identity Framework için AppUser implementasyonu
/// Bu sınıf IdentityUser'dan miras alır ve Infrastructure katmanında kalır
/// Domain AppUser ile mapping yapılır
/// </summary>
public class IdentityAppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<IdentityAppUserRole> UserRoles { get; set; } = new List<IdentityAppUserRole>();


    public IdentityAppUser()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
    }
}