using System.Threading.Tasks;

namespace VehicleDashboard.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
