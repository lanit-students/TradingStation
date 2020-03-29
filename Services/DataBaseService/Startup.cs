using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using DataBaseService.Utils;
using DataBaseService.Interfaces;
using DataBaseService.DbModels;
using DataBaseService.Repositories;
using DataBaseService.Mappers;

using DTO;
using DataBaseService.Commands;

namespace DataBaseService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }       

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            var migrationEngine = new MigrationEngine(Configuration);
            migrationEngine.Migrate();

            services.AddControllers();

            services.AddDbContext<DataBaseContext>();

            services.AddTransient<IRepository<UserEmailPassword>, UserCredentialRepository>();
            services.AddTransient<IMapper<UserEmailPassword, DbUserCredential>, UserCredentialMapper>();
            services.AddTransient<ICommand<UserEmailPassword>, CreateUserCommand>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
