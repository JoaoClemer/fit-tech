using FitTech.Application.Services.Cryptography;
using FitTech.Application.Services.LoggedUser;
using FitTech.Application.Services.Token;
using FitTech.Application.UseCases.Employee.Create;
using FitTech.Application.UseCases.Gym.Create;
using FitTech.Application.UseCases.Login.ChangePassword;
using FitTech.Application.UseCases.Login.DoLogin;
using FitTech.Application.UseCases.Plan.Create;
using FitTech.Application.UseCases.Student.Create;
using FitTech.Application.UseCases.Student.GetAllStudentsOfGym;
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
            AddLoggedUser(services);
        }
        private static void AddEncryptKey(this IServiceCollection services, IConfiguration configuration)
        {
            var encryptKey = configuration.GetSection("Configuration:Password:EncryptKey").Value;
            services.AddScoped(option => new PasswordEncryptor(encryptKey));
        }
        private static void AddTokenJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var lifeTime = int.Parse(configuration.GetSection("Configuration:Token:LifeTimeToken").Value);
            var tokenKey = configuration.GetSection("Configuration:Token:TokenKey").Value;

            services.AddScoped(option => new TokenController( lifeTime, tokenKey));
        }

        private static void AddLoggedUser(this IServiceCollection services)
        {
            services.AddScoped<ILoggedUser, LoggedUser>();
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateGymUseCase, CreateGymUseCase>()
               .AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>()
               .AddScoped<ICreateStudentUseCase, CreateStudentUseCase>()
               .AddScoped<IDoLoginUseCase, DoLoginUseCase>()
               .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>()
               .AddScoped<ICreatePlanUseCase, CreatePlanUseCase>()
               .AddScoped<IGetAllStudentsOfGymUseCase, GetAllStudentsOfGymUseCase>();
        }

    }
}
