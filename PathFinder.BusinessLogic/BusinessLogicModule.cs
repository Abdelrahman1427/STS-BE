using Autofac;
using STS.BusinessLogic.Services.Shared;
using STS.Core.Interface.Shared.IServices;
using STS.SharedKernel.Interfaces;

namespace STS.BusinessLogic
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
