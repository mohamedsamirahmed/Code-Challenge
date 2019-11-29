using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.Core.Common.UnitOfWork;
using VehicleDashboard.VehicleService.Domain.Helpers;
using VehicleDashboard.VehicleService.DTO;

namespace VehicleDashboard.VehicleService.Domain.Services
{
   public interface IVehicleDashboardService : IUnitOfWork
    {
        //ResponseModel<IEnumerable<CustomerVehiclesDTO>> GetCustomerVehicleList();
        Task<PagedList<CustomerVehiclesDTO>> GetCustomerVehicleList(CustomerVehicleParams customerVehicleParams);

        Task UpdateCustomerVehicleStatus(CustomerVehiclesDTO customerVehiclesDto);

    }
}
