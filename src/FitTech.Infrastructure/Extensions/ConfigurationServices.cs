using FitTech.Domain.Repositories;
using FitTech.Domain.Repositories.Gym;
using FitTech.Infrastructure.Context;
using FitTech.Infrastructure.RepositoryAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitTech.Infrastructure.Extensions
{
    public static class ConfigurationServices
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            AddContext(service, configuration);
            AddRepositories(service);
            AddUnitOfWork(service);
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
                .AddScoped<IGymWriteOnlyRepository,GymRepository>();
        }

        private static void AddUnitOfWork(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
