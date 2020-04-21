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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OperationService.Commands;
using System;
using System.Collections.Generic;

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
                x.AddRequestClient<GetInstrumentsRequest>(new Uri("rabbitmq://localhost/BrokerService"));
            });

            services.AddMassTransitHostedService();

            services.AddTransient<ICommand<GetInstrumentsRequest, IEnumerable<Instrument>>, GetInstrumentsCommand>();

            services.AddLogging(log =>
            {
                log.ClearProviders();
            });

            services.AddTransient<ILoggerProvider, LoggerProvider>(provider =>
            {
                return new LoggerProvider(provider);
            });
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
                endpoints.MapControllers();
            });
        }
    }
}
