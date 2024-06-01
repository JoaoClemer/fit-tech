namespace FitTech.Api
{
    public static class ApiRoutes
    {
        private const string Version = "v1/";
        private const string Base = "api/" + Version;

        public static class Login
        {
            public const string DoLogin = Base + "login/";

            public const string ChangePassword = Base + "changePassword/";
        }

        public static class Plan
        {
            public const string CreatePlan = Base + "plan/";
        }

        public static class Employee
        {
            public const string CreateEmployee = Base + "employee/";
        }

        public static class Student
        {
            public const string CreateStudent = Base + "student/";

            public const string GetAllStudentsOfGym = Base + "student/";

            public const string GetStudentById = Base + "student/{studentId}";
        }

        public static class Gym
        {
            public const string CreateGym = Base + "gym/";
        }

        public static class Dashboard
        {
            public const string GetStudentDashboard = Base + "dashboard/student";
        }
    }
}
