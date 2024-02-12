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
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(async services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(FitTechContext));
                    if(descriptor != null)
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
            var employee = EmployeeSeedDataFactory.BuildSimpleEmployee();
            _employee.EmailAddress = employee.EmailAddress;
            _employee.Password = employee.Password;

            employee.Gym = await context.Gyms.FirstAsync();
            employee.Password = PasswordEncryptorBuilder.Instance().Encrypt(_employee.Password);
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
        }

        public Student GetStudent() { return _student; }

        public Employee GetEmployee() { return _employee; }
    }
}
