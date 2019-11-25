using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.EventBus.Events;

namespace VehiclesDashboard.VehicleConnection.API.IntegrationEvents
{
    public interface ICustomerVehicleHistoryIntegrationEventService
    {
      //  Task SaveEventAndCustomerVehicleHistoryContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
