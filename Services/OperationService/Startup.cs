using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
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
            	var brokerUri = new Uri("rabbitmq://localhost/BrokerService");
                var databaseUri = new Uri("rabbitmq://localhost/DatabaseService");

                x.AddBus(provider => CreateBus(provider));

               	x.AddConsumer<CandleConsumer>();

                x.AddRequestClient<GetInstrumentsRequest>(brokerUri);
                x.AddRequestClient<InternalTradeRequest>(brokerUri);
                x.AddRequestClient<Transaction>(databaseUri);
                x.AddRequestClient<GetInstrumentFromPortfolioRequest>(databaseUri);
                x.AddRequestClient<GetUserBalanceRequest>(databaseUri);
                x.AddRequestClient<UserBalance>(databaseUri);
				x.AddRequestClient<GetCandlesRequest>(brokerUri);
                x.AddRequestClient<GetPortfolioRequest>(databaseUri);
                x.AddRequestClient<GetUserTransactions>(databaseUri);
                x.AddRequestClient<CreateBotRequest>(databaseUri);
                x.AddRequestClient<DeleteBotRequest>(databaseUri);
                x.AddRequestClient<RunBotRequest>(databaseUri);
                x.AddRequestClient<EditBotRequest>(databaseUri);
                x.AddRequestClient<DisableBotRequest>(databaseUri);
                x.AddRequestClient<InternalGetBotsRequest>(databaseUri);
                x.AddRequestClient<InternalSaveRuleRequest>(databaseUri);
                x.AddRequestClient<InternalGetBotRulesRequest>(databaseUri);
            });

            services.AddMassTransitHostedService();

            services.AddTransient<ICommand<GetInstrumentsRequest, IEnumerable<Instrument>>, GetInstrumentsCommand>();
			services.AddTransient<ICommand<TradeRequest, bool>, TradeCommand>();
            services.AddTransient<ICommand<GetInstrumentFromPortfolioRequest, Instrument>, GetInstrumentFromPortfolioCommand>();
            services.AddTransient<ICommand<GetUserBalanceRequest, UserBalance>, GetUserBalanceCommand>();
            services.AddTransient<ICommand<UpdateUserBalanceRequest, bool>, UpdateUserBalanceCommand>();
            services.AddTransient<ICommand<GetPortfolioRequest, List<InstrumentData>>, GetPortfolioCommand>();
            services.AddTransient<ICommand<GetUserTransactionsRequest, IEnumerable<Transaction>>, GetUserTransactionsCommand>();
            services.AddTransient<ICommand<GetCandlesRequest, IEnumerable<Candle>>, GetCandlesCommand>();
			services.AddTransient<ICommand<CreateBotRequest, bool>, CreateBotCommand>();

            services.AddTransient<ICommand<DeleteBotRequest, bool>, DeleteBotCommand>();

            services.AddTransient<ICommand<EditBotRequest, bool>, EditBotCommand>();

            services.AddTransient<ICommand<RunBotRequest, bool>, RunBotCommand>();

            services.AddTransient<ICommand<DisableBotRequest, bool>, DisableBotCommand>();

            services.AddTransient<ICommand<Guid, List<BotData>>, GetBotsCommand>();

            services.AddTransient<ICommand<InternalSaveRuleRequest, bool>, SaveBotRuleCommand>();

            //services.AddLogging(log =>
            //{
            //    log.ClearProviders();
            //});

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
