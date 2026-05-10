namespace SchoolManagementSystem.Data.ApplicationMetadata
{
    public static class Router
    {
        public const string Route = "/api";
        public const string Version = "/v1";
        public const string SingelRoute = "/{id}";
        public const string Rule = $"{Route}{Version}/";

        public static class StudentRouting
        {
            public const string Prefix = $"{Rule}student";

            public const string GetAllStudents = $"{Prefix}s/";
            public const string GetStudentById = Prefix + SingelRoute;
            public const string Create = Prefix + "/create";
            public const string Update = Prefix + "/update";
            public const string Delete = Prefix + "/delete";
        }

        // prefix: /api/v1/student
        // api/v1/students
        // api/v1/student/{id}
    }
}
