using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace STS.BusinessLogic.Mapping
{
    public static class AutoMapperContainer
    {
        private static IHttpContextAccessor _context;
        public static IMapper CreateMapper(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            return new MapperConfiguration(cfg =>
            {

                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new CategoryProfile());
                cfg.AddProfile(new CartItemProfile());


            }).CreateMapper();
        }
    }
}