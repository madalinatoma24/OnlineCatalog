using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions
{
    public static class DataExtension
    {
        public static void AddDataAccessLayer(this IServiceCollection services, Action<DbContextOptionsBuilder> contextBuilder) 
        {
            services.AddDbContext<StudentsDbContext>(contextBuilder, ServiceLifetime.Scoped);

            services.AddScoped<IStudentDbContext>(s => s.GetService<StudentsDbContext>());
            services.AddScoped<DataAccesLayer>();
        }
    }
}
