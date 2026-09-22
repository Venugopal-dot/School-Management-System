using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.Auth;
using School.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        return Ok(new ApiResponse<AuthResponse>
        {
            Success = true,
            Message = "User registered successfully.",
            Data = result
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        return Ok(new ApiResponse<AuthResponse>
        {
            Success = true,
            Message = "Login successful.",
            Data = result
        });
    }

    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new
        {
            Success = true,
            Message = "You have access to the Admin endpoint."
        });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(
    [FromBody] ForgotPasswordRequest request)
    {
        await _authService.ForgotPasswordAsync(request);

        return Ok(new
        {
            success = true,
            message = "Password reset request processed successfully."
        });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
    [FromBody] ResetPasswordRequest request)
    {
        await _authService.ResetPasswordAsync(request);

        return Ok(new
        {
            success = true,
            message = "Password reset successfully."
        });
    }
}