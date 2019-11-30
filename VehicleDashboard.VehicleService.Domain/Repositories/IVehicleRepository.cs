using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.Domain.Repositories
{
    public interface IVehicleRepository : IEntityFrameworkRepository<Vehicle>
    {
    }
}
