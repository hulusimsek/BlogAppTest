using AutoMapper;
using BlogApp.Application.DTOs.Auth;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Mappings;

/// <summary>
/// AutoMapper Profile for mapping between entities and DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()))
            .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Will be handled separately

        // Role mappings
        CreateMap<AppRole, string>()
            .ConvertUsing(role => role.Name);
    }
}