using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleDashboard.Core.Common.UnitOfWork;

namespace VehicleDashboard.VehicleConnection.Domain.Services
{
    public interface ICustomerVehicleHistoryService : IUnitOfWork
    {
        Task AddCustomerVehicleHistory(VehicleConnection.DTO.CustomerVehicleHistoryDTO customerVehicleHistoryDto);
    }
}
