namespace School.Domain.Common
{
    public class SubjectCodeGenerator
    {
        public static string Generate(int id)
        {
            return $"SUB{id:D5}";
        }
    }
}
