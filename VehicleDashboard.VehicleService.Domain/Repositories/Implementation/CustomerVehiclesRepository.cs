using System;
using System.Collections.Generic;
using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.Domain.Repositories.Implementation
{
    public class CustomerVehiclesRepository : EntityFrameworkRepository<CustomerVehicle>,ICustomerVehiclesRepository
    {
        private readonly VehicleServiceDataContext _dbContext;
        public CustomerVehiclesRepository(VehicleServiceDataContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
