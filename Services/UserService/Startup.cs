using IDeleteUserUserService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Commands;
using UserService.Interfaces;
using MassTransit;
using GreenPipes;
using MassTransit.AspNetCoreIntegration;
using Microsoft.Extensions.Configuration;
using UserService.BrokerConsumers;
using System;
using Kernel.Middlewares;
using Kernel;
using UserService.Validators;
using DTO.RestRequests;
using FluentValidation;

namespace UserService
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

            string serviceName = Configuration.GetSection(serviceSection)["Name"] ?? "UserService";

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

                    ep.ConfigureConsumer<LoginUserConsumer>(serviceProvider);
                });
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddControllers();

            services.AddTransient<IValidator<DeleteUserRequest>, DeleteUserRequestValidator>();

            services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();

            services.AddTransient<IGetUserByIdCommand, GetUserByIdCommand>();

            services.AddTransient<ICreateUserCommand, CreateUserCommand> ();
            services.AddTransient<IValidator<CreateUserRequest>, CreateUserRequestValidator>();

            services.AddTransient<IEditUserCommand, EditUserCommand>();
            services.AddTransient<IValidator<UserInfoRequest>, UserInfoRequestValidator>();
            services.AddTransient<IValidator<PasswordChangeRequest>, PasswordChangeRequestValidator>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<LoginUserConsumer>();
                x.AddBus(provider => CreateBus(provider));
            });

            services.AddMassTransitHostedService();
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
            app.UseMiddleware<CheckTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
