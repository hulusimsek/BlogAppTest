using AutoMapper;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Application.Mappings;
using BlogApp.Domain.Repositories;
using BlogApp.Persistence.Mappings;
using BlogApp.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            // sadece Persistence assembly'sindeki profilleri kaydeder
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RoleMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<UserRoleMappingProfile>();
            });

            // Repository pattern

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISiteSettingsRepository, SiteSettingsRepository>();

            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
            services.AddScoped<IHomePageSectionRepository, HomePageSectionRepository>();
            services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
            services.AddScoped<IContactRequestRepository, ContactRequestRepository>();
            services.AddScoped<IContactFormSettingsRepository, ContactFormSettingsRepository>();
            services.AddScoped<ILawyerProfileRepository, LawyerProfileRepository>();
            services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
            services.AddScoped<ISiteSettingsRepository, SiteSettingsRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            // Repository ve UnitOfWork kayıtları buraya da eklenir
            // örn: services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
