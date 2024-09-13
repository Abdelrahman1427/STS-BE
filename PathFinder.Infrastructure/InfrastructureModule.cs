using Autofac;
using PathFinder.Core.Interfaces.Shared.Repository;
using PathFinder.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Module = Autofac.Module;

namespace PathFinder.Infrastructure
{
    public class InfrastructureModule<T> : Module where T : DbContext
    {
        // IOC Container Methods
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork<T>>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
