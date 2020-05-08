using DTO;
using DTO.BrokerRequests;
using GreenPipes;
using Interfaces;
using Kernel;
using Kernel.LoggingEngine;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OperationService.Commands;
using System;
using System.Collections.Generic;
using OperationService.BrokerConsumers;
using OperationService.Hubs;
using DTO.RestRequests;

namespace OperationService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IBusControl CreateBus(IServiceProvider serviceProvider)
        {
            const string serviceSection = "ServiceInfo";

            string serviceId = Configuration.GetSection(serviceSection)["ID"] ?? Guid.NewGuid().ToString();

            string serviceName = Configuration.GetSection(serviceSection)["Name"] ?? "AuthService";

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", hst =>
                {
                    hst.Username($"{serviceName}_{serviceId}");
                    hst.Password($"{serviceId}");
                });

                cfg.ReceiveEndpoint($"{serviceName}", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));

                    ep.ConfigureConsumer<CandleConsumer>(serviceProvider);
                });
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddControllers();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => CreateBus(provider));

                x.AddConsumer<CandleConsumer>();

                x.AddRequestClient<GetInstrumentsRequest>(new Uri("rabbitmq://localhost/BrokerService"));
                x.AddRequestClient<GetCandlesRequest>(new Uri("rabbitmq://localhost/BrokerService"));

                x.AddRequestClient<CreateBotRequest>(new Uri("rabbitmq://localhost/DataBaseService"));
                x.AddRequestClient<DeleteBotRequest>(new Uri("rabbitmq://localhost/DataBaseService"));
                x.AddRequestClient<RunBotRequest>(new Uri("rabbitmq://localhost/DataBaseService"));
                x.AddRequestClient<DisableBotRequest>(new Uri("rabbitmq://localhost/DataBaseService"));
                x.AddRequestClient<BotInfoRequest>(new Uri("rabbitmq://localhost/DataBaseService"));

            });

            services.AddMassTransitHostedService();

            services.AddTransient<ICommand<GetInstrumentsRequest, IEnumerable<Instrument>>, GetInstrumentsCommand>();

            services.AddTransient<ICommand<GetCandlesRequest, IEnumerable<Candle>>, GetCandlesCommand>();


            services.AddTransient<ICommand<CreateBotRequest, bool>, CreateBotCommand>();

            services.AddTransient<ICommand<DeleteBotRequest, bool>, DeleteBotCommand>();
            
            services.AddTransient<ICommand<RunBotRequest, bool>, RunBotCommand>();

            services.AddTransient<ICommand<DisableBotRequest, bool>, DisableBotCommand>();

            services.AddTransient<ICommand<Guid, bool>, GetBotsCommand>();

            services.AddLogging(log =>
            {
                log.ClearProviders();
            });

            services.AddTransient<ILoggerProvider, LoggerProvider>(provider =>
            {
                return new LoggerProvider(provider);
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(CustomExceptionHandler.HandleCustomException);
            });

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<CandleHub>("/CandleHub");
                endpoints.MapControllers();
            });
        }
    }
}
