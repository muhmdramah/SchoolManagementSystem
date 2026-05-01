using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Service.Implementations;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            return services;
        }
    }
}
