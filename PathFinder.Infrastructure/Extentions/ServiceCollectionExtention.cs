using STS.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace STS.Infrastructure.Extentions
{
    public static class ServiceCollectionExtention
    {
        // extention for bulider.service
        public static void AddDbContextClient(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<STSDBContext>(options =>
            {
                options.UseSqlServer(connectionString, b =>
                {
                    b.MigrationsAssembly(typeof(STSDBContext).Assembly.FullName)
                     .UseNetTopologySuite();
                });
            });

    }
}
