using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.EventBus.Abstractions;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleService.Domain.Services;
using VehicleDashboard.VehicleService.DTO;

namespace VehiclesDashboard.VehicleServices.API.IntegrationEvents.EventHandler
{
    public class CustomerVehicleChangedIntegrationEventHandler :
     IIntegrationEventHandler<CustomerVehicleChangedIntegrationEvent>
    {
        private IVehicleDashboardService _customerVehicleService;
        private readonly ILogger<CustomerVehicleChangedIntegrationEventHandler> _logger;

        public CustomerVehicleChangedIntegrationEventHandler(
            ILogger<CustomerVehicleChangedIntegrationEventHandler> logger,
            IVehicleDashboardService customerVehicleHistoryService
            )
        {
            _customerVehicleService = customerVehicleHistoryService;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// add new item into customer vehicle history list
        /// </summary>
        /// <param name="customerVehicleEvent">customer event history item DTO</param>
        /// <returns></returns>
        public async Task Handle(CustomerVehicleChangedIntegrationEvent customerVehicleEventMessage)
        {
            try
            {
                await _customerVehicleService.UpdateCustomerVehicleStatus(customerVehicleEventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }
    }
}
