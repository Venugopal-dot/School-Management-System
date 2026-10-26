using Microsoft.Extensions.Logging;
using School.Application.Interfaces;

namespace School.Infrastructure.Services;

public class DevelopmentEmailService : IEmailService
{
    private readonly ILogger<DevelopmentEmailService> _logger;

    public DevelopmentEmailService(
        ILogger<DevelopmentEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendPasswordResetEmailAsync(
    string email,
    string resetToken)
    {
        _logger.LogInformation(
            "PASSWORD RESET EMAIL | To: {Email} | Reset Token: {ResetToken}",
            email,
            resetToken);

        return Task.CompletedTask;
    }
}