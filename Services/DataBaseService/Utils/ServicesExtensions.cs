using DataBaseService.BrokerConsumers;
using MassTransit.ExtensionsDependencyInjectionIntegration;

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
			service.AddConsumer<ConfirmUserConsumer>();
			service.AddConsumer<CreateBotConsumer>();


            return service;
        }
    }
}
