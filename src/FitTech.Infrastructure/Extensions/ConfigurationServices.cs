using FitTech.Infrastructure.Context;
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
        }

        private static void AddContext(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetFullConnection();
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

            service.AddDbContext<FitTechContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });

        }
    }
}
