using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.External.GoogleBooks;
using School.Application.Interfaces;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoogleBooksController : ControllerBase
{
    private readonly IGoogleBooksService _googleBooksService;

    public GoogleBooksController(IGoogleBooksService googleBooksService)
    {
        _googleBooksService = googleBooksService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Search query is required.",
                Data = null
            });
        }

        var books = await _googleBooksService.SearchBooksAsync(query);

        return Ok(new ApiResponse<IEnumerable<GoogleBookDto>>
        {
            Success = true,
            Message = "Books retrieved successfully.",
            Data = books
        });
    }
}