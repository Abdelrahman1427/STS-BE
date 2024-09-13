using Autofac;
using STS.Core.Interfaces.Shared.Repository;
using STS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Module = Autofac.Module;

namespace STS.Infrastructure
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
