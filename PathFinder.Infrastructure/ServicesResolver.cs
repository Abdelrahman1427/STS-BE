using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STS.BusinessLogic.Services.IClient;
using STS.BusinessLogic.Services.Shared;
using STS.Core.Interface.IClientServices;
using STS.Core.Interface.Shared.IServices;


namespace STS.Infrastructure
{
    internal static class ServicesResolver
    {
        internal static void ResolveDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IGovernorateService, GovernorateService>();
           
            services.AddScoped(typeof(IGetViewUpdateCrudService<>), typeof(GetViewUpdateCrudService<>));

        }
    }
}
