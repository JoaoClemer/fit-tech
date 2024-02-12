using FitTech.Application.Services.Cryptography;
using FitTech.Application.Services.Token;
using FitTech.Application.UseCases.Employee.Create;
using FitTech.Application.UseCases.Gym.Create;
using FitTech.Application.UseCases.Login.DoLogin;
using FitTech.Application.UseCases.Student.Create;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitTech.Application.Extensions
{
    public static class ConfigurationServices
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddEncryptKey(services, configuration);
            AddTokenJWT(services, configuration);
            AddUseCases(services);
        }
        public static void AddEncryptKey(this IServiceCollection services, IConfiguration configuration)
        {
            var encryptKey = configuration.GetSection("Configuration:Password:EncryptKey").Value;
            services.AddScoped(option => new PasswordEncryptor(encryptKey));
        }
        public static void AddTokenJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var lifeTime = int.Parse(configuration.GetSection("Configuration:Token:LifeTimeToken").Value);
            var tokenKey = configuration.GetSection("Configuration:Token:TokenKey").Value;

            services.AddScoped(option => new TokenController( lifeTime, tokenKey));
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateGymUseCase, CreateGymUseCase>()
               .AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>()
               .AddScoped<ICreateStudentUseCase, CreateStudentUseCase>()
               .AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }

    }
}
