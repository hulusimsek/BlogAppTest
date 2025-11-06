using BlogApp.Domain.Entities;
using BlogApp.Persistence.Configurations;
using BlogApp.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

    public DbSet<SiteSettings> SiteSettings { get; set; }
    public DbSet<LawyerProfile> LawyerProfiles { get; set; }
    public DbSet<PageSection> PageSection { get; set; }
    public DbSet<LawyerSpecialization> LawyerSpecializations { get; set; }
    public DbSet<CareerHistory> CareerHistories { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceDetail> ServiceDetails { get; set; }
    public DbSet<ProcessFlowStep> ProcessFlowSteps { get; set; }
    public DbSet<ServiceFaqItem> ServiceFaqItems { get; set; }
    public DbSet<TestimonialItem> TestimonialItems { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<BlogFaqItem> BlogFaqItems { get; set; }
    public DbSet<BlogComment> BlogComments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BlogPostTag> BlogPostTags { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<ContactInfoFaqItem> ContactInfoFaqItems { get; set; }
    public DbSet<WorkingHour> WorkingHours { get; set; }
    public DbSet<ContactRequest> ContactRequests { get; set; }
    public DbSet<ContactFormSettings> ContactFormSettings { get; set; }
    public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }

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

        builder.ApplyConfiguration(new SiteSettingsConfiguration());
        builder.ApplyConfiguration(new LawyerProfileConfiguration());
        builder.ApplyConfiguration(new LawyerSpecializationConfiguration());
        builder.ApplyConfiguration(new PageSectionConfiguration());
        builder.ApplyConfiguration(new CareerHistoryConfiguration());
        builder.ApplyConfiguration(new ServiceCategoryConfiguration());
        builder.ApplyConfiguration(new ServiceDetailConfiguration());
        builder.ApplyConfiguration(new ProcessFlowStepConfiguration());
        builder.ApplyConfiguration(new ServiceFaqItemConfiguration());
        builder.ApplyConfiguration(new BlogFaqItemConfiguration());
        builder.ApplyConfiguration(new ContactInfoFaqItemConfiguration());
        builder.ApplyConfiguration(new TestimonialItemConfiguration());
        builder.ApplyConfiguration(new BlogPostConfiguration());
        builder.ApplyConfiguration(new BlogCommentConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new BlogPostTagConfiguration());
        builder.ApplyConfiguration(new ContactInfoConfiguration());
        builder.ApplyConfiguration(new ContactRequestConfiguration());
        builder.ApplyConfiguration(new AppointmentRequestConfiguration());
        builder.ApplyConfiguration(new MenuItemConfiguration());

    }

}