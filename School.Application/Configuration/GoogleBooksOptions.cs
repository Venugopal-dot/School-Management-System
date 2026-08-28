namespace School.Application.Configuration;

public class GoogleBooksOptions
{
    public const string SectionName = "ExternalApis:GoogleBooks";

    public string BaseUrl { get; set; } = string.Empty;
}