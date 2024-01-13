using FitTech.Application.UseCases.Employee.Create;
using FitTech.Application.UseCases.Gym.Create;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTech.Application.Extensions
{
    public static class ConfigurationServices
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateGymUseCase, CreateGymUseCase>()
                .AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>();
        }
    }
}
