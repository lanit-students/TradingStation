using DataBaseService.BrokerConsumers;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Utils
{
    public static class ServicesExtensions
    {
        public static IServiceCollectionConfigurator AddUserConsumers(this IServiceCollectionConfigurator service)
        {
            if (service == null)
                return null;

            service.AddConsumer<CreateUserConsumer>();
            service.AddConsumer<LoginUserConsumer>();
            service.AddConsumer<DeleteUserConsumer>();
            service.AddConsumer<GetUserByIdConsumer>();
            service.AddConsumer<EditUserConsumer>();

            return service;
        }
    }
}
