using System.Threading.Tasks;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.Core.Common.UnitOfWork;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleConnection.Domain.Helpers;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehicleDashboard.VehicleConnection.Domain.Services
{
    public interface ICustomerVehicleHistoryService : IUnitOfWork
    {
        //Task AddCustomerVehicleHistory(CustomerVehicleHistoryDTO customerVehicleHistoryDto);
        Task AddCustomerVehicleHistory(CustomerVehicleChangedIntegrationEvent customerVehicleHistoryEventMessage);
        Task<ResponseModel<PagedList<CustomerVehicleHistoryDTO>>> GetCustomerVehicleHistory(string vehicleId, int customerId, string regNo, CustomerVehicleHistoryParams customerVehicleHistoryParams);
    }
}
