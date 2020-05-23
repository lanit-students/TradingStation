using LogReader.Database;
using LogReader.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Windows;

namespace LogReader
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = new HostBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<LogViewModel>();

                services.AddDbContext<LogContext>(options =>
                {
                    var connection = ConfigurationManager.ConnectionStrings["TradingStationLogsString"].ConnectionString;
                    options.UseSqlServer(connection);
                });

                services.AddSingleton<LogsWindow>();
            }).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var window = services.GetRequiredService<LogsWindow>();

                window.Show();
            }
        }
    }
}
