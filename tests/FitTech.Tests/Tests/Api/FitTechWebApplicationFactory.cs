﻿using FitTech.Infrastructure.Context;
using FitTech.Tests.Utils.SeedDataFactory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitTech.Tests.Tests.Api
{
    public class FitTechWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
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
            await context.Gyms.AddAsync(GymSeedDataFactory.BuildSimpleGym());

            context.SaveChanges();
        }
    }
}
