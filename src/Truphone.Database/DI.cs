using Microsoft.Extensions.DependencyInjection;
using System;
using Truphone.Database.Bootstrap;
using Truphone.Database.Repositories;
using Truphone.Domain.Adapters;

namespace Truphone.Database
{
    public static class DI
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddSingleton<BootstrapInMemoryDB>();
            services.AddScoped<DbSession>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceProvider UseInMemoryDatabase(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<BootstrapInMemoryDB>().Setup();
            return serviceProvider;
        }
    }
}
