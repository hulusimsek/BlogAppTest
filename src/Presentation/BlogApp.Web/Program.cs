using BlogApp.Application.Configuration;
using BlogApp.Application.Interfaces;
using BlogApp.Application.Interfaces.Infrastructure;
using BlogApp.Application.Interfaces.Web;
using BlogApp.Application.Mappings;
using BlogApp.Domain.Repositories;
using BlogApp.Infrastructure.DependencyInjection;
using BlogApp.Infrastructure.Services;
using BlogApp.Persistence.Data;
using BlogApp.Persistence.DependencyInjection;
using BlogApp.Persistence.Identity;
using BlogApp.Persistence.Repositories;
using BlogApp.Persistence.Seed;
using BlogApp.Web.Middlewares;
using BlogApp.Web.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 39))));

// Identity
builder.Services.AddIdentity<IdentityAppUser, BlogApp.Persistence.Identity.IdentityAppRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Email confirmation
    options.SignIn.RequireConfirmedEmail = false; // Development için false
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cookie Authentication for MVC
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// Application Services
builder.Services.AddApplicationServices();

// Persistence Services
builder.Services.AddPersistenceServices();

// Infrastructure Services
builder.Services.AddInfrastructureServices();

builder.Services.AddScoped<IFileStorageService, FileStorageService>();

builder.Services.AddMemoryCache();


// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ModeratorOrAdmin", policy => policy.RequireRole("Admin", "Moderator"));
});

// Session (optional - eðer session kullanmak isterseniz)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// TempData (for flash messages)
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

// HTTP Context Accessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session (eðer eklediyseniz)
app.UseSession();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Database migration and seeding
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        // Data seeding
        // Debug için
        Console.WriteLine("Starting data seed...");
        await DataSeeder.SeedAsync(context);
        Console.WriteLine("Data seed completed.");

        // Role seeding
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<BlogApp.Persistence.Identity.IdentityAppRole>>();
        await SeedRolesAsync(roleManager);

        // Admin account seeding
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BlogApp.Persistence.Identity.IdentityAppUser>>();
        await SeedAdminAccount(userManager, configuration);

        Console.WriteLine("Database migration and seeding completed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration failed: {ex.Message}");
    }
}

app.Run();

// Helper method for seeding roles
static async Task SeedRolesAsync(RoleManager<BlogApp.Persistence.Identity.IdentityAppRole> roleManager)
{
    string[] roles = { "Admin", "User", "Moderator" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new BlogApp.Persistence.Identity.IdentityAppRole(role, $"{role} role"));
            Console.WriteLine($"Role created: {role}");
        }
    }
}

static async Task SeedAdminAccount(UserManager<BlogApp.Persistence.Identity.IdentityAppUser> userManager, IConfiguration configuration)
{
    var adminEmail = configuration["AdminEmail"];
    var adminPassword = configuration["AdminPassword"];
    var adminUser = await userManager.FindByEmailAsync(adminEmail ?? "");
    string adminRoleName = "Admin";

    if (adminUser == null)
    {
        adminUser = new IdentityAppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (!createResult.Succeeded)
        {
            Console.WriteLine("Admin kullanýcý oluþturulamadý.");
            return;
        }

        Console.WriteLine("Admin kullanýcý oluþturuldu.");
    }

    var isInRole = await userManager.IsInRoleAsync(adminUser, adminRoleName);
    if (!isInRole)
    {
        await userManager.AddToRoleAsync(adminUser, adminRoleName);
        Console.WriteLine("Admin kullanýcýsýna Admin rolü atandý.");
    }
}