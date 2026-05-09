using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Core.Behaviors;
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

            // FluentValidation registrations
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
