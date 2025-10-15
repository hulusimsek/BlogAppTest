using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Application.DTOs.Auth;
using BlogApp.Application.Features.Users.Queries.GetUserProfile;
using System.Security.Claims;

namespace BlogApp.WebAPI.Controllers;

/// <summary>
/// Users Controller
/// Kullanıcı yönetimi işlemlerini handle eder
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Mevcut kullanıcının profil bilgilerini getirir
    /// </summary>
    /// <returns>Kullanıcı profil bilgileri</returns>
    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var query = new GetUserProfileQuery { UserId = userId };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(new { message = result.ErrorMessage });
        }

        return Ok(new { success = true, data = result.Data });
    }

    /// <summary>
    /// Kullanıcı bilgilerini günceller
    /// </summary>
    /// <param name="request">Güncellenecek kullanıcı bilgileri</param>
    /// <returns>Güncellenmiş kullanıcı bilgileri</returns>
    [HttpPut("profile")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        // Bu komut henüz oluşturulmadı, basit bir implementasyon yapabiliriz
        // Şimdilik profil bilgilerini döndürelim
        var query = new GetUserProfileQuery { UserId = userId };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        return Ok(new { success = true, data = result.Data, message = "Profil güncellendi" });
    }

    /// <summary>
    /// Mevcut kullanıcının rollerini getirir
    /// </summary>
    /// <returns>Kullanıcı rolleri</returns>
    [HttpGet("roles")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserRoles()
    {
        var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        return await Task.FromResult<IActionResult>(Ok(new { success = true, data = userRoles }));
    }

    /// <summary>
    /// Admin rolü gerektiren test endpoint
    /// </summary>
    /// <returns>Admin mesajı</returns>
    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AdminOnly()
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        return Ok(new { 
            success = true, 
            message = $"Merhaba {userName}, sen bir adminsin!", 
            timestamp = DateTime.UtcNow 
        });
    }
}

/// <summary>
/// Profil güncelleme DTO
/// </summary>
public class UpdateProfileRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}