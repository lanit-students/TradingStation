using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using DataBaseService.Utils;
using DataBaseService.Interfaces;
using DataBaseService.DbModels;
using DataBaseService.Repositories;
using DataBaseService.Mappers;
using DataBaseService.Commands;

using DTO;

using MassTransit;
using System;
using DatabaseService.BrokerConsumers;
using GreenPipes;
using DataBaseService.BrokerConsumers;

namespace DataBaseService
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

            string serviceName = Configuration.GetSection(serviceSection)["Name"] ?? "DatabaseService";

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", hst =>
                {
                    hst.Username($"{serviceName}_{serviceId}");
                    hst.Password($"{serviceId}");
                });

                cfg.ReceiveEndpoint($"{serviceName}Login", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));

                    ep.ConfigureConsumer<UserLoginConsumer>(serviceProvider);
                });

                cfg.ReceiveEndpoint($"{serviceName}Create", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));

                    ep.ConfigureConsumer<UserCreationConsumer>(serviceProvider);
                });
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationEngine = new MigrationEngine(Configuration);
            migrationEngine.Migrate();

            services.AddControllers();

            services.AddDbContext<DataBaseContext>();

            services.AddTransient<IRepository<UserEmailPassword>, UserCredentialRepository>();
            services.AddTransient<IMapper<UserEmailPassword, DbUserCredential>, UserCredentialMapper>();
            services.AddTransient<ICommand<UserEmailPassword>, CreateUserCommand>();

            //var migrationEngine = new MigrationEngine(Configuration);
            //migrationEngine.Migrate();

            services.AddHealthChecks();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => CreateBus(provider));
                x.AddConsumer<UserCreationConsumer>();
                x.AddConsumer<UserLoginConsumer>();
            });

            services.AddMassTransitHostedService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
