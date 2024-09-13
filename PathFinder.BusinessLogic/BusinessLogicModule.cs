using Autofac;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.Core.Interface.Shared.IServices;
using PathFinder.SharedKernel.Interfaces;

namespace PathFinder.BusinessLogic
{
    public class BusinessLogicModule : Module
    {
        // IOC Container Method
        protected override void Load(ContainerBuilder builder)
        {

            //Services
            builder.RegisterType<LoggerService>().As<ILoggerService>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<LoggerFactoryService>().As<ILoggerFactoryService>()
                    .InstancePerLifetimeScope();

        }
    }
}
