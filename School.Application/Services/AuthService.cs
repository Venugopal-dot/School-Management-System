using Microsoft.AspNetCore.Identity;
using School.Application.DTOs.Auth;
using School.Application.Exceptions;
using School.Application.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace School.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _emailService;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtTokenService jwtTokenService,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName))
        {
            throw new BusinessValidationException(
                "User name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new BusinessValidationException(
                "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new BusinessValidationException(
                "Password is required.");
        }

        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new DuplicateRecordException(
                "User with this email already exists.");
        }

        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            Role = "Student"
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.Password);

        user = await _userRepository.AddAsync(user);

        var token = _jwtTokenService.GenerateToken(
            user.Id,
            user.UserName,
            user.Email,
            user.Role);

        return new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Role = user.Role,
            Token = token
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new BusinessValidationException(
                "Invalid email or password.");
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new BusinessValidationException(
                "Invalid email or password.");
        }

        var token = _jwtTokenService.GenerateToken(
            user.Id,
            user.UserName,
            user.Email,
            user.Role);

        return new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Role = user.Role,
            Token = token
        };
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new BusinessValidationException("Email is required.");

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            throw new NotFoundException("User not found.");

        var resetToken = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(32));

        var resetTokenHash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(resetToken)));

        //user.PasswordResetToken = resetToken;
        user.PasswordResetTokenHash = resetTokenHash;
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(15);

        await _userRepository.UpdateAsync(user);
        await _emailService.SendPasswordResetEmailAsync(user.Email, resetToken);
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new BusinessValidationException("Email is required.");

        if (string.IsNullOrWhiteSpace(request.ResetToken))
            throw new BusinessValidationException("Reset token is required.");

        if (string.IsNullOrWhiteSpace(request.NewPassword))
            throw new BusinessValidationException("New password is required.");

        if (request.NewPassword.Length < 8)
            throw new BusinessValidationException(
                "New password must be at least 8 characters long.");

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            throw new BusinessValidationException("Invalid password reset request.");

        var resetTokenHash = Convert.ToBase64String(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(request.ResetToken)));

        if (string.IsNullOrWhiteSpace(user.PasswordResetTokenHash) ||
            user.PasswordResetTokenHash != resetTokenHash)
        {
            throw new BusinessValidationException(
                "Invalid password reset request.");
        }

        if (!user.PasswordResetTokenExpiry.HasValue ||
            user.PasswordResetTokenExpiry.Value < DateTime.UtcNow)
        {
            throw new BusinessValidationException(
                "Password reset token has expired.");
        }

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.NewPassword);

        // Invalidate the token after successful reset
        //user.PasswordResetToken = null;
        user.PasswordResetTokenHash = null;
        user.PasswordResetTokenExpiry = null;

        await _userRepository.UpdateAsync(user);
    }
}