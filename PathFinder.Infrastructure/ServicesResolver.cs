using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STS.BusinessLogic.Services.IClient;
using STS.Core.Interface.IClientServices;


namespace STS.Infrastructure
{
    internal static class ServicesResolver
    {
        internal static void ResolveDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IGovernorateService, GovernorateService>();

        }
    }
}
