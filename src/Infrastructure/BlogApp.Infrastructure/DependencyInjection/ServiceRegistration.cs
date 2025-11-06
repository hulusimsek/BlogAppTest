using BlogApp.Application.Interfaces;
using BlogApp.Application.Interfaces.Infrastructure;
using BlogApp.Application.Interfaces.Web;
using BlogApp.Infrastructure.Services;
using BlogApp.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFaviconService, FaviconService>();

            return services;
        }
    }
}
