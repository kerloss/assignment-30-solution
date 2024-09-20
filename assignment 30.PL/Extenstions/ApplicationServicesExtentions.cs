using assignment_20.BLL.Interfacies;
using assignment_20.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace assignment_30.PL.Extenstions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationsServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
        //OR
        //public static void AddApplicationsServices(this IServiceCollection services)
        //{
        //    services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        //    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        //}
    }
}
