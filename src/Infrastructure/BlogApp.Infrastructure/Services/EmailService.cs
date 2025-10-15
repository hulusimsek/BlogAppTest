using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BlogApp.Application.Interfaces;

namespace BlogApp.Persistence.Services;

/// <summary>
/// Email Service Implementation
/// Gerçek uygulamada SendGrid, AWS SES, Azure Communication Services vb. kullanılır
/// </summary>
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailConfirmationAsync(string email, string confirmationLink)
    {
        try
        {
            // Gerçek uygulamada buraya email provider entegrasyonu yapılır
            _logger.LogInformation($"Email confirmation sent to: {email}");
            _logger.LogInformation($"Confirmation link: {confirmationLink}");

            // Simüle edilen async işlem
            await Task.Delay(100);

            // SendGrid örneği:
            /*
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_configuration["SendGrid:FromEmail"], "BlogApp");
            var to = new EmailAddress(email);
            var subject = "Email Adresinizi Onaylayın";
            var htmlContent = $@"
                <h2>Email Adresinizi Onaylayın</h2>
                <p>Hesabınızı aktifleştirmek için aşağıdaki linke tıklayın:</p>
                <a href='{confirmationLink}'>Email Adresimi Onayla</a>
            ";
            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            await client.SendEmailAsync(msg);
            */
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Email confirmation could not be sent to: {email}");
            throw;
        }
    }

    public async Task SendPasswordResetAsync(string email, string resetLink)
    {
        try
        {
            _logger.LogInformation($"Password reset email sent to: {email}");
            _logger.LogInformation($"Reset link: {resetLink}");

            await Task.Delay(100);

            // Gerçek email gönderimi burada yapılır
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Password reset email could not be sent to: {email}");
            throw;
        }
    }

    public async Task SendWelcomeEmailAsync(string email, string firstName)
    {
        try
        {
            _logger.LogInformation($"Welcome email sent to: {email} for user: {firstName}");

            await Task.Delay(100);

            // Gerçek email gönderimi burada yapılır
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Welcome email could not be sent to: {email}");
            // Welcome email kritik değil, throw etmiyoruz
        }
    }
}