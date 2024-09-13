using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STS.BusinessLogic.Services.DefinitionServices;
using STS.BusinessLogic.Services.IClient;
using STS.BusinessLogic.Services.Shared;
using STS.Core.Interface.IDefinitionServices;
using STS.Core.Interface.Shared.IServices;


namespace STS.Infrastructure
{
    internal static class ServicesResolver
    {
        internal static void ResolveDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartItemService, CartItemService>();

            services.AddScoped(typeof(IGetViewUpdateCrudService<>), typeof(GetViewUpdateCrudService<>));

        }
    }
}
