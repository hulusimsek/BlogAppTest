using MediatR;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Auth;

namespace BlogApp.Application.Features.Users.Queries.GetUserProfile;

/// <summary>
/// Get User Profile Query
/// </summary>
public class GetUserProfileQuery : IRequest<Result<UserDto>>
{
    public Guid UserId { get; set; }
}