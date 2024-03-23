using FitTech.Domain.Entities;
using FitTech.Infrastructure.Context;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.SeedDataFactory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitTech.Tests.Tests.Api
{
    public class FitTechWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private Student _student = new Student();
        private Employee _employee = new Employee();
        private Employee _admEmployee = new Employee();
        private Plan _plan = new Plan();
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
            _student.EmailAddress = student.EmailAddress;
            _student.Password = student.Password;


            student.Gym = await context.Gyms.FirstAsync();
            student.Password = PasswordEncryptorBuilder.Instance().Encrypt(student.Password);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        private async void SeedEmployees(FitTechContext context)
        {
            var employees = new List<Employee>();

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

        public Student GetStudent() { return _student; }

        public Employee GetEmployee() { return _employee; }

        public Employee GetAdmEmployee() { return _admEmployee; }

        public Plan GetPlan() { return _plan; }
    }
}
