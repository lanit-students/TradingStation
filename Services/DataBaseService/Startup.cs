using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using DataBaseService.Database;
using DataBaseService.BrokerConsumers;
using DataBaseService.Utils;
using DataBaseService.Repositories;
using DataBaseService.Mappers;

using MassTransit;
using GreenPipes;
using DataBaseService.Repositories.Interfaces;
using DataBaseService.Mappers.Interfaces;

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

                cfg.ReceiveEndpoint($"{serviceName}", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));

                    ep.ConfigureConsumer<CreateUserConsumer>(serviceProvider);
                    ep.ConfigureConsumer<LoginConsumer>(serviceProvider);
                });
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationEngine = new MigrationEngine(Configuration);
            migrationEngine.Migrate();

            services.AddHealthChecks();

            services.AddControllers();

            services.AddDbContext<TPlatformDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TradingStationString"));
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserMapper, UserMapper>();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => CreateBus(provider));
                x.AddConsumer<CreateUserConsumer>();
                x.AddConsumer<LoginConsumer>();
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
