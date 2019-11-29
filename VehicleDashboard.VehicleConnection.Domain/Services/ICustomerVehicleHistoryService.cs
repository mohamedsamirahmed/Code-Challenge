using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.Core.Common.UnitOfWork;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehicleDashboard.VehicleConnection.Domain.Services
{
    public interface ICustomerVehicleHistoryService : IUnitOfWork
    {
        Task AddCustomerVehicleHistory(CustomerVehicleHistoryDTO customerVehicleHistoryDto);
        ResponseModel<IEnumerable<CustomerVehicleHistoryDTO>> GetCustomerVehicleHistory(string vehicleId, int customerId, string regNo); 
    }
}
