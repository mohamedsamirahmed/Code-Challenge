﻿using VehicleDashboard.Core.Common.Repository;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.Domain.Repositories
{
    public interface ICustomerRepository : IEntityFrameworkRepository<Customer>
    {
    }
}
