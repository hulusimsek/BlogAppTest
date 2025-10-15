using AutoMapper;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Identity;


namespace BlogApp.Persistence.Mappings
{
    // kullanmaya gerek yok ama yazdım
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {

            // Domain AppUserRole -> Persistence IdentityAppUserRole
            CreateMap<AppUserRole, IdentityAppUserRole>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => src.AssignedDate))
                .ForMember(dest => dest.User, opt => opt.Ignore()) // Navigationlar manuel yüklenir
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            // Persistence IdentityAppUserRole -> Domain AppUserRole
            CreateMap<IdentityAppUserRole, AppUserRole>()
                .ConstructUsing(src => new AppUserRole(src.UserId, src.RoleId))
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => src.AssignedDate))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
