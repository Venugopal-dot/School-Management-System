using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using School.Application.DTOs.External.GoogleBooks;
using School.Application.Exceptions;
using School.Application.Interfaces;

namespace School.Application.Services;

public class GoogleBooksService : IGoogleBooksService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GoogleBooksService> _logger;

    public GoogleBooksService(HttpClient httpClient, ILogger<GoogleBooksService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<GoogleBookDto>> SearchBooksAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Enumerable.Empty<GoogleBookDto>();
        }

        _logger.LogInformation(
            "Searching Google Books API for query: {Query}",
            query);

        var response = await _httpClient.GetAsync(
            $"volumes?q={Uri.EscapeDataString(query)}");

        _logger.LogInformation(
            "Google Books API returned status code: {StatusCode}",
            response.StatusCode);

        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            _logger.LogWarning(
                "Google Books API quota exceeded for query: {Query}",
                query);

            throw new ExternalApiException(
                "Google Books API quota has been exceeded. Please try again later.");
        }

        if ((int)response.StatusCode >= 500)
        {
            _logger.LogError(
                "Google Books API returned server error: {StatusCode}",
                response.StatusCode);

            throw new ExternalApiException(
                "Google Books API is currently unavailable. Please try again later.");
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "Google Books API request failed with status code: {StatusCode}",
                response.StatusCode);

            throw new ExternalApiException(
                "Unable to retrieve books from Google Books API.");
        }

        var result = await response.Content
            .ReadFromJsonAsync<GoogleBooksApiResponse>();

        if (result?.Items == null)
        {
            _logger.LogInformation(
                "Google Books API returned no books for query: {Query}",
                query);

            return Enumerable.Empty<GoogleBookDto>();
        }

        return result.Items.Select(x => new GoogleBookDto
        {
            Title = x.VolumeInfo?.Title ?? string.Empty,

            Authors = x.VolumeInfo?.Authors != null
                ? string.Join(", ", x.VolumeInfo.Authors)
                : string.Empty,

            PublishedDate = x.VolumeInfo?.PublishedDate ?? string.Empty,

            Description = x.VolumeInfo?.Description ?? string.Empty,

            Thumbnail = x.VolumeInfo?.ImageLinks?.Thumbnail ?? string.Empty,

            PreviewLink = x.VolumeInfo?.PreviewLink ?? string.Empty
        });
    }

    private class GoogleBooksApiResponse
    {
        public List<GoogleBookItem>? Items { get; set; }
    }

    private class GoogleBookItem
    {
        public GoogleBookVolumeInfo? VolumeInfo { get; set; }
    }

    private class GoogleBookVolumeInfo
    {
        public string? Title { get; set; }

        public List<string>? Authors { get; set; }

        public string? PublishedDate { get; set; }

        public string? Description { get; set; }

        public GoogleBookImageLinks? ImageLinks { get; set; }

        public string? PreviewLink { get; set; }
    }

    private class GoogleBookImageLinks
    {
        public string? Thumbnail { get; set; }
    }
}