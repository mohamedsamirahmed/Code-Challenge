using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.Domain.Repositories.Implementation
{
    public class CustomerRepository : EntityFrameworkRepository<Customer>, ICustomerRepository
    {
        private readonly VehicleServiceDataContext _dbContext;
        public CustomerRepository(VehicleServiceDataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
