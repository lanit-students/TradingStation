using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
                    ep.ConfigureConsumer<ConfirmUserConsumer>(serviceProvider);

                    ep.ConfigureConsumer<TransactionConsumer>(serviceProvider);
                    ep.ConfigureConsumer<GetInstrumentFromPortfolioConsumer>(serviceProvider);
                    ep.ConfigureConsumer<GetUserBalanceConsumer>(serviceProvider);
                    ep.ConfigureConsumer<UpdateUserBalanceConsumer>(serviceProvider);
                    ep.ConfigureConsumer<GetPortfolioConsumer>(serviceProvider);
                    ep.ConfigureConsumer<GetTransactionConsumer>(serviceProvider);
                    ep.ConfigureConsumer<CreateBotConsumer>(serviceProvider);
                    ep.ConfigureConsumer<DeleteBotConsumer>(serviceProvider);
                    ep.ConfigureConsumer<RunBotConsumer>(serviceProvider);
                    ep.ConfigureConsumer<DisableBotConsumer>(serviceProvider);
                    ep.ConfigureConsumer<BotInfoConsumer>(serviceProvider);
                    ep.ConfigureConsumer<GetBotRulesConsumer>(serviceProvider);
                    ep.ConfigureConsumer<SaveBotRuleConsumer>(serviceProvider);
                    ep.ConfigureConsumer<EditBotConsumer>(serviceProvider);
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
			services.AddTransient<ITradeRepository, TradeRepository>();
            services.AddTransient<IBotRepository, BotRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IBotRuleRepository, BotRuleRepository>();
            services.AddTransient<IUserMapper, UserMapper>();
            services.AddTransient<ITradeMapper, TradeMapper>();
            services.AddTransient<IBotRuleMapper, BotRuleMapper>();
            services.AddTransient<IBotMapper, BotMapper>();
			services.AddTransient<ITradeMapper, TradeMapper>();
            services.AddTransient<ILogMapper, LogMapper>();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => CreateBus(provider));

                x.AddUserConsumers();
				x.AddConsumer<TransactionConsumer>();
                x.AddConsumer<GetInstrumentFromPortfolioConsumer>();
                x.AddConsumer<GetUserBalanceConsumer>();
                x.AddConsumer<UpdateUserBalanceConsumer>();
                x.AddConsumer<GetPortfolioConsumer>();
                x.AddConsumer<AddLogConsumer>();
                x.AddConsumer<GetTransactionConsumer>();
				x.AddConsumer<CreateBotConsumer>();
                x.AddConsumer<DeleteBotConsumer>();
                x.AddConsumer<RunBotConsumer>();
                x.AddConsumer<DisableBotConsumer>();
                x.AddConsumer<BotInfoConsumer>();
                x.AddConsumer<SaveBotRuleConsumer>();
                x.AddConsumer<AddLogConsumer>();
                x.AddConsumer<GetBotRulesConsumer>();
                x.AddConsumer<EditBotConsumer>();
            });

            services.AddMassTransitHostedService();

            //services.AddLogging(log =>
            //{
            //    log.ClearProviders();
            //});

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
