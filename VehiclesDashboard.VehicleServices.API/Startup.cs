using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
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
using System;
using System.Net;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.EventBus;
using VehicleDashboard.EventBus.Abstractions;
using VehicleDashboard.EventBusRabbitMQ;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Services;
using VehicleDashboard.VehicleService.Domain.Services.Implementation;
using VehiclesDashboard.VehicleServices.API.IntegrationEvents.EventHandler;

namespace VehiclesDashboard.VehicleServices.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add service and create Policy with options
            services.AddCors();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<VehicleServiceDataContext>(db => db.UseSqlite(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("VehiclesDashboard.VehicleServices.API")));
            services.AddTransient<IVehicleDashboardService, VehicleDashboardService>();
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            return ConfigureAutofac(services);

        }

        private IServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            // services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
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

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<CustomerVehicleChangedIntegrationEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
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

            //adding signalR hub context
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<VehicleMonitoringHub>("");
            //});

            app.UseMvc();
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<CustomerVehicleChangedIntegrationEvent, CustomerVehicleChangedIntegrationEventHandler>();
        }

    }
}
