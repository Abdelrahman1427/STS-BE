using PathFinder.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PathFinder.Infrastructure.Extentions
{
    public static class ServiceCollectionExtention
    {
        // extention for bulider.service
        public static void AddDbContextClient(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<PathFinderDBContext>(options =>
            {
                options.UseSqlServer(connectionString, b =>
                {
                    b.MigrationsAssembly(typeof(PathFinderDBContext).Assembly.FullName)
                     .UseNetTopologySuite();
                });
            });

    }
}
