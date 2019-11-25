using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using VehicleDashboard.EventBus;
using VehicleDashboard.EventBus.Abstractions;
using VehicleDashboard.EventBusRabbitMQ;
using VehicleDashboard.SPA.Helpers;
using VehicleDashboard.VehicleConnection.Data;
using VehicleDashboard.VehicleConnection.Domain.Services;
using VehicleDashboard.VehicleConnection.Domain.Services.Implementation;
using VehiclesDashboard.VehicleConnection.API.IntegrationEvents;
using VehiclesDashboard.VehicleConnection.API.IntegrationEvents.EventHandling;

namespace VehiclesDashboard.VehicleConnection.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add service and create Policy with options
            services.AddCors();
            services.AddDbContext<VehicleConnectionHistoryDataContext>(db => db.UseSqlite(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("VehiclesDashboard.VehicleConnection.API")));
            services.AddTransient<ICustomerVehicleHistoryIntegrationEventService, CustomerVehicleHistoryIntegrationEventService>();
            services.AddTransient<ICustomerVehicleHistoryService, CustomerVehicleHistoryService>();

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return ConfigureAutofac(services);

        }

        /// <summary>
        /// consifure autofac into container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private  IServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        private void RegisterEventBus(IServiceCollection services)
        {

            var subscriptionClientName = _configuration.GetValue<string>("EventBusSettings:SubscriptionClientName");

                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(_configuration.GetValue<string>("EventBusSettings:EventBusRetryCount")))
                    {
                        retryCount = int.Parse(_configuration.GetValue<string>("EventBusSettings:EventBusRetryCount"));
                    }

                    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            

           
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<CustomerVehicleChangedIntegrationEvent, CustomerVehicleChangedIntegrationEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }

                    });
                });
            }

            //allow cors origin
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            //configure customer vehicle status on change
            ConfigureEventBus(app);
            app.UseMvc();
        }
    }
}
