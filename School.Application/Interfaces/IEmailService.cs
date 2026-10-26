namespace School.Application.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(
        string email,
        string resetToken);
}