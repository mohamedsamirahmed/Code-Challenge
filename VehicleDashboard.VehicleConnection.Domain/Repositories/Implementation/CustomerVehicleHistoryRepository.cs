using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public  IQueryable<CustomerVehicleHistory> GetGustomerVehicleHistory(int customerId, string vehicleId, string regNo)
        {
            try
            {
                return _dbContext.CustomerehicleHistory.Where(c => (c.CustomerId == customerId)
                                              && (string.Compare(c.RegNo, regNo, true) == 0)
                                              && (string.Compare(c.VehicleId,vehicleId,true)==0));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
