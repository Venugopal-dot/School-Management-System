namespace School.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(
        int userId,
        string userName,
        string email,
        string role);
}