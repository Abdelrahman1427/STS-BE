using PathFinder.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API
{
    public class MigrateDatabase
    {
        public async static Task EnsureMigration(WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<PathFinderDBContext>();
                await context.Database.MigrateAsync();
            }
        }
    }
}
