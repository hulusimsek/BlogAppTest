using MediatR;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Application.DTOs.Auth;
using BlogApp.Application.Features.Auth.Commands.ForgotPassword;
using BlogApp.Application.Features.Auth.Commands.Login;
using BlogApp.Application.Features.Auth.Commands.Register;
using BlogApp.Application.Features.Auth.Commands.ResetPassword;

namespace BlogApp.WebAPI.Controllers;

/// <summary>
/// Authentication Controller
/// Kimlik doğrulama işlemlerini yönetir
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Kullanıcı girişi
    /// </summary>
    /// <param name="request">Giriş bilgileri</param>
    /// <returns>JWT token ve kullanıcı bilgileri</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var command = new LoginCommand
        {
            EmailOrUserName = request.EmailOrUserName,
            Password = request.Password
        };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage, errors = result.Errors });
        }

        return Ok(new { success = true, data = result.Data });
    }

    /// <summary>
    /// Yeni kullanıcı kaydı
    /// </summary>
    /// <param name="request">Kayıt bilgileri</param>
    /// <returns>JWT token ve kullanıcı bilgileri</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var command = new RegisterCommand
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword
        };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage, errors = result.Errors });
        }

        return CreatedAtAction(nameof(Register), new { success = true, data = result.Data });
    }

    /// <summary>
    /// Şifremi unuttum
    /// </summary>
    /// <param name="request">Email bilgisi</param>
    /// <returns>İşlem sonucu</returns>
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
    {
        var command = new ForgotPasswordCommand
        {
            Email = request.Email
        };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage, errors = result.Errors });
        }

        return Ok(new { success = true, message = "Şifre sıfırlama linki email adresinize gönderildi" });
    }

    /// <summary>
    /// Şifre sıfırlama
    /// </summary>
    /// <param name="request">Şifre sıfırlama bilgileri</param>
    /// <returns>İşlem sonucu</returns>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        var command = new ResetPasswordCommand
        {
            Email = request.Email,
            Token = request.Token,
            NewPassword = request.NewPassword,
            ConfirmPassword = request.ConfirmPassword
        };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage, errors = result.Errors });
        }

        return Ok(new { success = true, message = "Şifreniz başarıyla güncellendi" });
    }

    /// <summary>
    /// Çıkış yap
    /// </summary>
    /// <returns>İşlem sonucu</returns>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        // JWT tabanlı sistemde logout client-side yapılır
        // Burada token'ı blacklist'e ekleyebilir veya refresh token'ı iptal edebiliriz
        return Ok(new { success = true, message = "Başarıyla çıkış yaptınız" });
    }
}