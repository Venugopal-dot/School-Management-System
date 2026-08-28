using School.Application.DTOs.External.GoogleBooks;

namespace School.Application.Interfaces;

public interface IGoogleBooksService
{
    Task<IEnumerable<GoogleBookDto>> SearchBooksAsync(string query);
}