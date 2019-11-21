using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Model;
using VehicleDashboard.VehicleService.Domain.Repositories;
using VehicleDashboard.VehicleService.Domain.Repositories.Implementation;
using VehicleDashboard.VehicleService.Domain.UnitOfWork;
using VehicleDashboard.VehicleService.DTO;

namespace VehicleDashboard.VehicleService.Domain.Services.Implementation
{
    public class VehicleDashboardService : IVehicleDashboardService
    {


        public ICustomerRepository customerRepo
        {
            get
            {
                if (_customerRepo == null)
                {
                    _customerRepo = new CustomerRepository(_dbContext);
                }
                return _customerRepo;
            }
        }

        private readonly VehicleServiceDataContext _dbContext;
        public ICustomerRepository _customerRepo;

        public VehicleDashboardService(VehicleServiceDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IEnumerable<CustomersDTO> GetCustomers()
        {
            List<CustomersDTO> customersDto = new List<CustomersDTO>();
            IQueryable<Customer> customers = customerRepo.GetAll();
            return CustomersDTO.MapFields(customers);
        }
    }
}
