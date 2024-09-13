
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace STS.Infrastructure
{
    public static class DependencyInjection
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration Configuration, List<Type> attributeTypes)
        {
            services.ResolveDependencies(Configuration);
        }
    } 
}