using Data;
using Data.Repositories;
using Domain.IRepositories;
using Domain.IRPA;
using Microsoft.Extensions.DependencyInjection;
using RPA;
namespace DI
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        public static void AddRPA(this IServiceCollection services)
        {
            services.AddScoped<ISearch, Search>();
        }
    }
}
