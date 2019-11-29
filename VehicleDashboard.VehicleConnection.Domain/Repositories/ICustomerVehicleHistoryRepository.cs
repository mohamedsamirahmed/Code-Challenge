using System.Collections.Generic;
using System.Linq;
using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleConnection.Domain.Model;

namespace VehicleDashboard.VehicleConnection.Domain.Repositories
{
    public interface ICustomerVehicleHistoryRepository : IEntityFrameworkRepository<CustomerVehicleHistory>
    {
        IQueryable<CustomerVehicleHistory>  GetGustomerVehicleHistory(int customerId, string vehicleId, string regNo);
    }
}
