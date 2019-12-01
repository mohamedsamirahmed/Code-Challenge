using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VehicleDashboard.EventBus.Abstractions;
using VehicleDashboard.EventBus.Events;

namespace VehicleDashboard.Simulator.HostScheduler.IntegrationEvents
{
    public class CustomerVehicleHistoryIntegrationEventService : ICustomerVehicleHistoryIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<CustomerVehicleHistoryIntegrationEventService> _logger;

        public CustomerVehicleHistoryIntegrationEventService(
            ILogger<CustomerVehicleHistoryIntegrationEventService> logger,
            IEventBus eventBus
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// publish specific event into event bus.
        /// </summary>
        /// <param name="evt">event required to publish</param>
        /// <returns></returns>
        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _eventBus.Publish(evt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
