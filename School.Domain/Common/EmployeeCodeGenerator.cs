namespace School.Domain.Common
{
    public static class EmployeeCodeGenerator
    {
        public static string Generate(int id)
        {
            return $"EMP{id:D5}";
        }
    }
}
