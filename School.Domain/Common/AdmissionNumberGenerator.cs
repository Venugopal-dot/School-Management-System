namespace School.Domain.Common;

public static class AdmissionNumberGenerator
{
    public static string Generate(int studentId)
    {
        return $"SCH-{DateTime.Now.Year}-{studentId:D6}";
    }
}