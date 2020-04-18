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
using DataBaseService.Database.Logs;
using DataBaseService.Database.Logs.Interfaces;
using Kernel.LoggingEngine;
using Microsoft.Extensions.Logging;

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
                    ep.ConfigureConsumer<DeleteUserConsumer>(serviceProvider);
                    ep.ConfigureConsumer<LoginUserConsumer>(serviceProvider);
                    ep.ConfigureConsumer<GetUserByIdConsumer>(serviceProvider);
                    ep.ConfigureConsumer<EditUserConsumer>(serviceProvider);
                });

                cfg.ReceiveEndpoint($"{serviceName}_Logs", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));

                    ep.ConfigureConsumer<AddLogConsumer>(serviceProvider);
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

            services.AddDbContext<TPlatformLogDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TradingStationLogsString"));
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUserMapper, UserMapper>();
            services.AddTransient<ILogMapper, LogMapper>();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => CreateBus(provider));

                x.AddUserConsumers();

                x.AddConsumer<AddLogConsumer>();
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
