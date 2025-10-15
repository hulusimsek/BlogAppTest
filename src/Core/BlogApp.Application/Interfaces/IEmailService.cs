namespace BlogApp.Application.Interfaces;

/// <summary>
/// Email Service Interface
/// </summary>
public interface IEmailService
{
    Task SendEmailConfirmationAsync(string email, string confirmationLink);
    Task SendPasswordResetAsync(string email, string resetLink);
    Task SendWelcomeEmailAsync(string email, string firstName);
}