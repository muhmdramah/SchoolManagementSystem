using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SchoolManagementSystem.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // MediatR registrations
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // AutoMapper registrations
            services.AddAutoMapper(cfg =>
                cfg.AddMaps(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
