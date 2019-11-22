using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDashboard.Shared.Common.Models;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Model;
using VehicleDashboard.VehicleService.Domain.Repositories;
using VehicleDashboard.VehicleService.Domain.Repositories.Implementation;
using VehicleDashboard.VehicleService.Domain.UnitOfWork;
using VehicleDashboard.VehicleService.DTO;
using Microsoft.EntityFrameworkCore;


namespace VehicleDashboard.VehicleService.Domain.Services.Implementation
{
    public class VehicleDashboardService : IVehicleDashboardService
    {
        public ICustomerVehiclesRepository customerVehiclesRepo
        {
            get
            {
                if (_customerVehiclesRepo == null)
                {
                    _customerVehiclesRepo = new CustomerVehiclesRepository(_dbContext);
                }
                return _customerVehiclesRepo;
            }
        }


        private readonly VehicleServiceDataContext _dbContext;
        public ICustomerVehiclesRepository _customerVehiclesRepo;

        public VehicleDashboardService(VehicleServiceDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public ResponseModel<IEnumerable<CustomerVehiclesDTO>> GetCustomerVehicleList()
        {
            ResponseModel<IEnumerable<CustomerVehiclesDTO>> returnResponse = new ResponseModel<IEnumerable<CustomerVehiclesDTO>>();
            List<CustomerVehiclesDTO> customerVehiclesListDTO = new List<CustomerVehiclesDTO>();
            try
            {
                IQueryable<CustomerVehicle> customerVehicles = customerVehiclesRepo.GetAll().Include(c=>c.Customer);
                customerVehiclesListDTO= CustomerVehiclesDTO.MapFields(customerVehicles);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }

            returnResponse.Entity = customerVehiclesListDTO;

            return returnResponse;

        }
    }

}
