using System.Threading.Tasks;
using VehicleDashboard.EventBus.Events;

namespace VehicleDashboard.Simulator.HostScheduler.IntegrationEvents
{
    public interface ICustomerVehicleHistoryIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
