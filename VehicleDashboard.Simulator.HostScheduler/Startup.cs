using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using VehicleDashboard.Simulator.HostScheduler.Jobs;

namespace VehicleDashboard.Simulator.HostScheduler
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Quartz services
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add customer vehicle job
            services.AddSingleton<CustomerVehiclesHistoryJob>();
            services.AddSingleton(new JobScheduleModel(
                jobType: typeof(CustomerVehiclesHistoryJob),
                cronExpression: _configuration.GetValue<string>("SchedulerJob:TimePeriodExperssion"))); // run every 1 Minuite

            services.AddHostedService<CustomeVehiclesHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           

            app.Run(async (context) =>
            {
                Random rnd = new Random();
                await context.Response.WriteAsync("Hello World!"+rnd.Next(0, 200).ToString());
            });
        }
    }
}
