using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleConnection.Data;
using VehicleDashboard.VehicleConnection.Domain.Model;

namespace VehicleDashboard.VehicleConnection.Domain.Repositories.Implementation
{
    public class CustomerVehicleHistoryRepository : EntityFrameworkRepository<CustomerVehicleHistory>, ICustomerVehicleHistoryRepository
    {
        private readonly VehicleConnectionHistoryDataContext _dbContext;
        public CustomerVehicleHistoryRepository(VehicleConnectionHistoryDataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
