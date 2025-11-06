using BlogApp.Application.Configuration;
using BlogApp.Application.Interfaces;
using BlogApp.Application.Mappings;
using BlogApp.Domain.Repositories;
using BlogApp.Infrastructure.DependencyInjection;
using BlogApp.Persistence.Data;
using BlogApp.Persistence.DependencyInjection;
using BlogApp.Persistence.Identity;
using BlogApp.Persistence.Repositories;
using BlogApp.Persistence.Seed;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

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

    // Email confirmation
    options.SignIn.RequireConfirmedEmail = false; // Development için false
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Application
builder.Services.AddApplicationServices();

// Persistence
builder.Services.AddPersistenceServices();



// Application services
builder.Services.AddInfrastructureServices();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? "your-super-secret-key-here-make-it-long-enough")),
        ValidateIssuer = true,
        ValidIssuer = configuration["Jwt:Issuer"] ?? "BlogApp",
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"] ?? "BlogApp-Users",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Authorization
builder.Services.AddAuthorization();


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Plant API - Identity Management",
        Version = "v1",
        Description = "ASP.NET Core Clean Architecture ile Identity Management API",
        Contact = new OpenApiContact
        {
            Name = "Plant API Team",
            Email = "info@BlogApp.com"
        }
    });

    // JWT Authentication için Swagger konfigürasyonu
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    // XML Comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plant API v1");
        c.RoutePrefix = string.Empty; // Swagger UI ana sayfada açılsın
    });
}

// Security headers (comment out HTTPS redirect for Replit)
// app.UseHsts();
// app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();

// Root endpoint
app.MapGet("/", () => new
{
    Message = "BlogApp - Clean Architecture Identity System",
    Version = "1.0.0",
    Status = "Running",
    Timestamp = DateTime.UtcNow,
    Environment = app.Environment.EnvironmentName,
    Endpoints = new
    {
        Swagger = "/swagger",
        Health = "/health",
        Auth = new
        {
            Login = "POST /api/auth/login",
            Register = "POST /api/auth/register",
            RefreshToken = "POST /api/auth/refresh-token",
            Logout = "POST /api/auth/logout",
            ForgotPassword = "POST /api/auth/forgot-password",
            ResetPassword = "POST /api/auth/reset-password"
        },
        Users = new
        {
            GetProfile = "GET /api/users/profile",
            UpdateProfile = "PUT /api/users/profile"
        }
    }
}).WithTags("Root");

// Health check endpoint
app.MapGet("/health", () => new
{
    Status = "Healthy",
    Timestamp = DateTime.UtcNow,
    Environment = app.Environment.EnvironmentName,
    Version = "1.0.0"
}).WithTags("Health");

// Database migration and seeding
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        // Data seeding
        await DataSeeder.SeedAsync(context);


        // Role seeding
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<BlogApp.Persistence.Identity.IdentityAppRole>>();
        await SeedRolesAsync(roleManager);

        Console.WriteLine("✅ Database migration completed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database migration failed: {ex.Message}");
    }

    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BlogApp.Persistence.Identity.IdentityAppUser>>();
        await SeedAdminAccount(userManager, builder);
    }
    catch(Exception ex)
    {
        Console.WriteLine($"❌ Database migration failed: {ex.Message}");

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
            Console.WriteLine($"✅ Role created: {role}");
        }
    }
}

static async Task SeedAdminAccount(UserManager<BlogApp.Persistence.Identity.IdentityAppUser> userManager, WebApplicationBuilder builder)
{
    var adminEmail = builder.Configuration["AdminEmail"];
    var adminPassword = builder.Configuration["AdminPassword"];
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
            Console.WriteLine("Admin kullanıcı oluşturulamadı.");
            return;
        }

        Console.WriteLine("✅ Admin kullanıcı oluşturuldu.");
    }

    // 3. Eğer admin kullanıcı "Admin" rolünde değilse, role ekle
    var isInRole = await userManager.IsInRoleAsync(adminUser, adminRoleName);
    if (!isInRole)
    {
        await userManager.AddToRoleAsync(adminUser, adminRoleName);
        Console.WriteLine("✅ Admin kullanıcısına Admin rolü atandı.");
    }

}