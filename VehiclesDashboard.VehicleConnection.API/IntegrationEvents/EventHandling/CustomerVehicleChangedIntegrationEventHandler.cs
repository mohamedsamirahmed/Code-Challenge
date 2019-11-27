﻿using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VehicleDashboard.EventBus.Abstractions;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleConnection.Domain.Services;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehiclesDashboard.VehicleConnection.API.IntegrationEvents.EventHandling
{
    public class CustomerVehicleChangedIntegrationEventHandler :
     IIntegrationEventHandler<CustomerVehicleChangedIntegrationEvent>
    {
        private ICustomerVehicleHistoryService _customerVehicleHistoryService;
        private readonly ILogger<CustomerVehicleChangedIntegrationEventHandler> _logger;
         
        public CustomerVehicleChangedIntegrationEventHandler(
            ILogger<CustomerVehicleChangedIntegrationEventHandler> logger,
            ICustomerVehicleHistoryService customerVehicleHistoryService
            )
        {
            _customerVehicleHistoryService = customerVehicleHistoryService;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// add new item into customer vehicle history list
        /// </summary>
        /// <param name="customerVehicleEvent">customer event history item DTO</param>
        /// <returns></returns>
        public async Task Handle(CustomerVehicleChangedIntegrationEvent customerVehicleEvent)
        {
            try
            {
                CustomerVehicleHistoryDTO customerVehicleDto = new CustomerVehicleHistoryDTO()
                {
                    ConnectionStatus = customerVehicleEvent.ConnectionStatus,
                    CustomerId = customerVehicleEvent.CustomerId,
                    ModificationStatus = customerVehicleEvent.ModificationStatus,
                    RegNo = customerVehicleEvent.RegNo,
                    VIN = customerVehicleEvent.VIN
                };

                await _customerVehicleHistoryService.AddCustomerVehicleHistory(customerVehicleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
           
        }
    }
}
