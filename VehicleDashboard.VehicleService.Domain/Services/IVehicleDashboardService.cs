using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDashboard.Shared.Common.Models;
using VehicleDashboard.VehicleService.Domain.UnitOfWork;
using VehicleDashboard.VehicleService.DTO;

namespace VehicleDashboard.VehicleService.Domain.Services
{
   public interface IVehicleDashboardService : IUnitOfWork
    {
        IEnumerable<CustomersDTO> GetCustomers();
    }
}
