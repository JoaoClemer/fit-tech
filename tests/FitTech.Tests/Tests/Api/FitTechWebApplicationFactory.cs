using FitTech.Domain.Entities;
using FitTech.Infrastructure.Context;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.SeedDataFactory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static FitTech.Api.ApiRoutes;

namespace FitTech.Tests.Tests.Api
{
    public class FitTechWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private Domain.Entities.Student _student = new Domain.Entities.Student();
        private Domain.Entities.Employee _employee = new Domain.Entities.Employee();
        private Domain.Entities.Employee _admEmployee = new Domain.Entities.Employee();
        private Domain.Entities.Plan _plan = new Domain.Entities.Plan();
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(async services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(FitTechContext));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<FitTechContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();

                    var database = scope.ServiceProvider.GetRequiredService<FitTechContext>();

                    database.Database.EnsureDeleted();

                    await SeedDataAsync(database);
                });
        }

        private async Task SeedDataAsync(FitTechContext context)
        {

            SeedGyms(context);
            SeedStudents(context);
            SeedStudentList(context);
            SeedEmployees(context);
            SeedPlans(context);

        }

        private async void SeedGyms(FitTechContext context)
        {
            await context.Gyms.AddAsync(GymSeedDataFactory.BuildSimpleGym());
            await context.SaveChangesAsync();
        }

        private async void SeedStudents(FitTechContext context)
        {
            var student = StudentSeedDataFactory.BuildSimpleStudent();
            _student.Name = student.Name;
            _student.EmailAddress = student.EmailAddress;
            _student.Password = student.Password;


            student.Gym = await context.Gyms.FirstAsync();
            student.Password = PasswordEncryptorBuilder.Instance().Encrypt(student.Password);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        private async void SeedStudentList(FitTechContext context)
        {
            var gym = await context.Gyms.FirstAsync();
            var students = StudentSeedDataFactory.BuildStudentList(5,5);

            students.ToList().ForEach(s => s.Gym = gym);
            students.ToList().ForEach(s => s.Password = PasswordEncryptorBuilder.Instance().Encrypt(s.Password));

            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }

        private async void SeedEmployees(FitTechContext context)
        {
            var employees = new List<Domain.Entities.Employee>();

            var simpleEmployee = EmployeeSeedDataFactory.BuildSimpleEmployee();
            _employee.EmailAddress = simpleEmployee.EmailAddress;
            _employee.Password = simpleEmployee.Password;

            employees.Add(simpleEmployee);

            var admEmployee = EmployeeSeedDataFactory.BuildAdministratorEmployee();
            _admEmployee.EmailAddress = admEmployee.EmailAddress;
            _admEmployee.Password = admEmployee.Password;

            employees.Add(admEmployee);

            foreach (var employee in employees)
            {
                employee.Gym = await context.Gyms.FirstAsync();
                employee.Password = PasswordEncryptorBuilder.Instance().Encrypt(employee.Password);
            }

            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();
        }

        private async void SeedPlans(FitTechContext context)
        {
            var plan = PlanSeedDataFactory.BuildPlan();
            plan.Gym = await context.Gyms.FirstAsync();

            _plan.Name = plan.Name;

            await context.Plans.AddAsync(plan);
            await context.SaveChangesAsync();
        }

        public Domain.Entities.Student GetStudent() { return _student; }

        public Domain.Entities.Employee GetEmployee() { return _employee; }

        public Domain.Entities.Employee GetAdmEmployee() { return _admEmployee; }

        public Domain.Entities.Plan GetPlan() { return _plan; }
    }
}
