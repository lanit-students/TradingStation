using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using FluentValidation;
using MassTransit;
using GreenPipes;

using AuthenticationService.BrokerConsumers;
using AuthenticationService.Commands;
using AuthenticationService.Interfaces;
using AuthenticationService.Validators;
using DTO;
using Kernel;
using DTO.BrokerRequests;
using Kernel.LoggingEngine;

namespace AuthenticationService
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

                    ep.ConfigureConsumer<TokenConsumer>(serviceProvider);
                });
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddControllers();

            services.AddSingleton<ITokensEngine, TokensEngine>();
            services.AddTransient<ILoginCommand, LoginCommand>();
            services.AddTransient<ILogoutCommand, LogoutCommand>();

            services.AddTransient<IValidator<UserToken>, UserTokenValidator>();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => CreateBus(provider));
                x.AddConsumer<TokenConsumer>();
                x.AddRequestClient<InternalLoginRequest>(new Uri("rabbitmq://localhost/UserService"));
            });

            services.AddMassTransitHostedService();

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
