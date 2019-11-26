using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using VehicleDashboard.Simulator.HostScheduler.IntegrationEvents;
using VehicleDashboard.Simulator.HostScheduler.IntegrationEvents.Events;

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
          

            //Create Integration Event to be published through the Event Bus
            var customerVehicleHistoryChangedEvent = new CustomerVehicleChangedIntegrationEvent("YS2R4X20005399401","ABC123",1,true,DateTime.Now);

            // Publish through the Event Bus and mark the saved event as published
             _customerVehicleHistoryIntegrationEventService.PublishThroughEventBusAsync(customerVehicleHistoryChangedEvent);

            return Task.CompletedTask;
        }
    }
}
