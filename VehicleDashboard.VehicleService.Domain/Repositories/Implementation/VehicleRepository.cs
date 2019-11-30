using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.Domain.Repositories.Implementation
{
    public class VehicleRepository:EntityFrameworkRepository<Vehicle>,IVehicleRepository
    {
        private readonly VehicleServiceDataContext _dbContext;
        public VehicleRepository(VehicleServiceDataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
