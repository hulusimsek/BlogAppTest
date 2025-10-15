using AutoMapper;
using MediatR;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;
using BlogApp.Domain.Repositories;

namespace BlogApp.Application.Features.Users.Queries.GetUserProfile;

/// <summary>
/// Get User Profile Query Handler
/// </summary>
public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        if (user == null)
        {
            return Result<UserDto>.Failure("Kullanıcı bulunamadı");
        }

        var userRoles = await _roleRepository.GetUserRolesAsync(user.Id);
        var roleNames = userRoles.Select(r => r.Name).ToList();

        var userDto = _mapper.Map<UserDto>(user);
        userDto.Roles = roleNames;
        userDto.FullName = user.GetFullName();

        return Result<UserDto>.Success(userDto);
    }
}