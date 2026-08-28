namespace School.Application.Exceptions;

public class ExternalApiException : Exception
{
    public ExternalApiException(string message) : base(message)
    {

    }
}