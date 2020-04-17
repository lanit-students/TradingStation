using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using FluentValidation;

using DTO.NewsRequests.Currency;
using NewsService.Validators;
using NewsService.Utils;
using NewsService.Interfaces;
using NewsService.Commands;
using Kernel;

namespace NewsService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IValidator<CurrencyRequest>, CurrencyRequestValidator>();
            services.AddTransient<IEqualityComparer<string>, RegisterIgnoreStringComparer>();

            services.AddTransient<IGetCurrenciesCommand, GetCurrenciesCommand>();
            services.AddTransient<IGetNewsCommand, GetNewsCommand>();
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


