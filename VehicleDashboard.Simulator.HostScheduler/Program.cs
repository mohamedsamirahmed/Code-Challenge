using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VehicleDashboard.Simulator.HostScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var serviceCollection = new ServiceCollection();
            //var serviceProvider = serviceCollection.BuildServiceProvider();
            //var _logger = serviceProvider.GetRequiredService<ILogger<Program>>();


            ILoggerFactory loggerFactory = new LoggerFactory()
   .AddConsole()
   .AddDebug().AddEventSourceLogger();
            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("This is a test of the emergency broadcast system.");
            CreateWebHostBuilder(args).Build().Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
