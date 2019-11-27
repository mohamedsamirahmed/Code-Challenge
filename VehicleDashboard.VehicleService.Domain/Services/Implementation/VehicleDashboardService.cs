using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDashboard.VehicleService.Data;
using VehicleDashboard.VehicleService.Domain.Model;
using VehicleDashboard.VehicleService.Domain.Repositories;
using VehicleDashboard.VehicleService.Domain.Repositories.Implementation;
using VehicleDashboard.VehicleService.DTO;
using Microsoft.EntityFrameworkCore;
using VehicleDashboard.Core.Common.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
        private ICustomerVehiclesRepository _customerVehiclesRepo;
        private readonly ILogger<VehicleDashboardService> _logger;

        public VehicleDashboardService(VehicleServiceDataContext dbContext,
           ILogger<VehicleDashboardService> 
            logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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


        public async Task UpdateCustomerVehicleStatus(CustomerVehiclesDTO customerVehiclesDto)
        {
            try
            {
                var CustomerVehicleEntity = customerVehiclesDto.GetEntity();
                customerVehiclesRepo.Update(CustomerVehicleEntity);
                await customerVehiclesRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }

}
