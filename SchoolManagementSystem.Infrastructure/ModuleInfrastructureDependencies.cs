using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Infrastructure.InfrastructureBases;
using SchoolManagementSystem.Infrastructure.Interfaces;
using SchoolManagementSystem.Infrastructure.Repositories;

namespace SchoolManagementSystem.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
