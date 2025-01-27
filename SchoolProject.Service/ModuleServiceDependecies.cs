using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependecies
    {
        public static IServiceCollection AddServiceDependecies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            return services;
        }
    }
}
