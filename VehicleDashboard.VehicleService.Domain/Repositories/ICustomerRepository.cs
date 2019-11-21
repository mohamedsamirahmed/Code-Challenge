using System.Collections.Generic;
using VehicleDashboard.VehicleService.Domain.Model;

namespace VehicleDashboard.VehicleService.Domain.Repositories
{
    public interface ICustomerRepository: IEntityFrameworkRepository<Customer>
    {
    }
}
