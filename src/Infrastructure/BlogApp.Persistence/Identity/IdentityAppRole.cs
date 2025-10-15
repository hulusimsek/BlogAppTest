using Microsoft.AspNetCore.Identity;

namespace BlogApp.Persistence.Identity;

/// <summary>
/// Identity Framework için Role implementasyonu
/// </summary>
public class IdentityAppRole : IdentityRole<Guid>
{
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }

    public virtual ICollection<IdentityAppUserRole> UserRoles { get; set; } = new List<IdentityAppUserRole>();


    public IdentityAppRole()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public IdentityAppRole(string roleName, string description) : this()
    {
        Name = roleName;
        NormalizedName = roleName.ToUpper();
        Description = description;
    }
}