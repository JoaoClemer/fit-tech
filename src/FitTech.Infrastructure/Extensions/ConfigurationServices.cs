using FitTech.Domain.Repositories;
using FitTech.Domain.Repositories.Employee;
using FitTech.Domain.Repositories.Gym;
using FitTech.Domain.Repositories.Student;
using FitTech.Infrastructure.Context;
using FitTech.Infrastructure.RepositoryAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FitTech.Infrastructure.Extensions
{
    public static class ConfigurationServices
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            AddContext(service, configuration);
            AddRepositories(service);
            AddUnitOfWork(service);
            AddSwaggerInfra(service);
        }

        private static void AddContext(IServiceCollection service, IConfiguration configuration)
        {
            bool.TryParse(configuration.GetSection("Configuration:DataBaseInMemory").Value, out bool dataBaseInMemory);

            if (!dataBaseInMemory)
            {
                var connectionString = configuration.GetFullConnection();
                var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

                service.AddDbContext<FitTechContext>(dbContextOptions =>
                {
                    dbContextOptions.UseMySql(connectionString, serverVersion);
                });
            }            

        }

        private static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IGymReadOnlyRepository,GymRepository>()
                .AddScoped<IGymWriteOnlyRepository,GymRepository>()
                .AddScoped<IEmployeeReadOnlyRepository, EmployeeRepository>()
                .AddScoped<IEmployeeWriteOnlyRepository, EmployeeRepository>()
                .AddScoped<IEmployeeUpdateOnlyRepository, EmployeeRepository>()
                .AddScoped<IStudentReadOnlyRepository, StudentRepository>()
                .AddScoped<IStudentWriteOnlyRepository, StudentRepository>()
                .AddScoped<IStudentUpdateOnlyRepository, StudentRepository>();
        }

        private static void AddUnitOfWork(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddSwaggerInfra(this IServiceCollection service)
        {
            service.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON WEB TOKEN"
                });

                s.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string [] {}
                    }
                });
            });
        }
    }
}
