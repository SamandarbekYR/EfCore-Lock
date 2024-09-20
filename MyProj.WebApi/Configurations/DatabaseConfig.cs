using Microsoft.EntityFrameworkCore;
using MyProj.WebApi.Data;

namespace MyProj.WebApi.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
