using FitTech.Application.Services.Cryptography;
using FitTech.Application.UseCases.Employee.Create;
using FitTech.Application.UseCases.Gym.Create;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitTech.Application.Extensions
{
    public static class ConfigurationServices
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var encryptKey = configuration.GetSection("Configuration:Password:EncryptKey").Value;

            services.AddScoped(option => new PasswordEncryptor(encryptKey));

            services.AddScoped<ICreateGymUseCase, CreateGymUseCase>()
                .AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>();
        }
    }
}
