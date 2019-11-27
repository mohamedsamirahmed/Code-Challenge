using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.Simulator.HostScheduler.Helpers;
using VehicleDashboard.Simulator.HostScheduler.IntegrationEvents;

namespace VehicleDashboard.Simulator.HostScheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class CustomerVehiclesHistoryJob : IJob
    {
        private readonly ILogger _logger;
        private readonly ICustomerVehicleHistoryIntegrationEventService _customerVehicleHistoryIntegrationEventService;

        public CustomerVehiclesHistoryJob(ILogger<CustomerVehiclesHistoryJob> logger,
            ICustomerVehicleHistoryIntegrationEventService customerVehicleHistoryIntegrationEventService)
        {
            _logger = logger;
            _customerVehicleHistoryIntegrationEventService = customerVehicleHistoryIntegrationEventService;
        }

        /// <summary>
        /// call service to add random history per each vehicle
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Start Generating Random Numbers using Simulator ");
            //Create Integration Event to be published through the Event Bus
            SimulatorHelper helper = new SimulatorHelper();
            List<CustomerVehicleChangedIntegrationEvent> customerVehiclesLst = helper.GenerateRandomStatus();
            foreach (var customerVehicleChangedEvent in customerVehiclesLst)
            {
                // Publish through the Event Bus and mark the saved event as published
                _customerVehicleHistoryIntegrationEventService.PublishThroughEventBusAsync(customerVehicleChangedEvent);
            }

            _logger.LogInformation("Generating Random Numbers using Simulator Completed ");

            return Task.CompletedTask;
        }


      
    }
}
