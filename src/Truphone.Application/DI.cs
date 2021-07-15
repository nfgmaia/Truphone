using Microsoft.Extensions.DependencyInjection;
using Truphone.Application.Services;
using Truphone.Domain.Services;

namespace Truphone.Application
{
    public static class DI
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IDeviceService, DeviceService>();
            return services;
        }
    }
}
