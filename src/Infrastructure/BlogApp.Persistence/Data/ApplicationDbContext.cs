using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Identity;

namespace BlogApp.Persistence.Data;

/// <summary>
/// Application Database Context
/// Identity Framework ve Domain entities için DbContext
/// </summary>
public class ApplicationDbContext : IdentityDbContext<IdentityAppUser, IdentityAppRole, Guid,
    IdentityUserClaim<Guid>, IdentityAppUserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Domain entities for business logic only (NOT mapped to database)
    // AppUser: Pure domain entity, mapped to IdentityAppUser
    
    // Domain roles and relationships (mapped to database)
    // public DbSet<AppRole> DomainRoles { get; set; } = null!;
    // public DbSet<AppUserRole> DomainUserRoles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Identity table names
        builder.Entity<IdentityAppUser>().ToTable("Users");
        builder.Entity<IdentityAppRole>().ToTable("Roles");
        builder.Entity<IdentityAppUserRole>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        builder.Entity<IdentityAppUserRole>(entity =>
        {
            entity.ToTable("UserRoles");

            entity.HasKey(x => new { x.UserId, x.RoleId });

            entity.HasOne(x => x.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(x => x.UserId) // ✅ FK'yi açıkça belirtiyoruz
                .IsRequired();

            entity.HasOne(x => x.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(x => x.RoleId) // ✅ FK'yi açıkça belirtiyoruz
                .IsRequired();
        });

    }

}