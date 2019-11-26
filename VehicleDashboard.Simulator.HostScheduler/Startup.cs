using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using RabbitMQ.Client;
using VehicleDashboard.EventBus;
using VehicleDashboard.EventBus.Abstractions;
using VehicleDashboard.EventBusRabbitMQ;
using VehicleDashboard.Simulator.HostScheduler.IntegrationEvents;
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add Quartz services
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddTransient<ICustomerVehicleHistoryIntegrationEventService, CustomerVehicleHistoryIntegrationEventService>();

            // Add customer vehicle job
            services.AddSingleton<CustomerVehiclesHistoryJob>();
            services.AddSingleton(new JobScheduleModel(
                jobType: typeof(CustomerVehiclesHistoryJob),
                cronExpression: _configuration.GetValue<string>("SchedulerJob:TimePeriodExperssion"))); // run every 1 Minuite
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = _configuration.GetValue<string>("EventBusSettings:EventBusConnection")
                };

                if (!string.IsNullOrEmpty(_configuration.GetValue<string>("EventBusSettings:EventBusUserName")))
                {
                    factory.UserName = _configuration.GetValue<string>("EventBusSettings:EventBusUserName");
                }

                if (!string.IsNullOrEmpty(_configuration.GetValue<string>("EventBusSettings:EventBusPassword")))
                {
                    factory.Password = _configuration.GetValue<string>("EventBusSettings:EventBusPassword");
                }

                var retryCount = 3;
                if (!string.IsNullOrEmpty(_configuration.GetValue<string>("EventBusSettings:EventBusRetryCount")))
                {
                    retryCount = _configuration.GetValue<int>("EventBusSettings:EventBusRetryCount");
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            RegisterEventBus(services);

            services.AddHostedService<CustomerVehiclesHostedService>();
            return ConfigureAutofac(services);
        }
      

       


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           

            //app.Run(async (context) =>
            //{
            //    Random rnd = new Random();
            //    await context.Response.WriteAsync("Hello World!"+rnd.Next(0, 200).ToString());
            //});


        }


        #region HelperMethods
        /// <summary>
        /// consifure autofac into container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        private void RegisterEventBus(IServiceCollection services)
        {

            var subscriptionClientName = _configuration.GetValue<string>("EventBusSettings:SubscriptionClientName");

            services.AddSingleton<IEventBus,EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(_configuration.GetValue<string>("EventBusSettings:EventBusRetryCount")))
                {
                    retryCount = int.Parse(_configuration.GetValue<string>("EventBusSettings:EventBusRetryCount"));
                }

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });
        }

        #endregion
    }
}
