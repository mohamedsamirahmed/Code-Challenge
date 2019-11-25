using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleConnection.Domain.Model;

namespace VehicleDashboard.VehicleConnection.Domain.Repositories
{
    public interface ICustomerVehicleHistoryRepository : IEntityFrameworkRepository<CustomerVehicleHistory>
    {
    }
}
