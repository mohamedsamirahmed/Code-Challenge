using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.VehicleConnection.Data;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.Domain.Repositories;
using VehicleDashboard.VehicleConnection.Domain.Repositories.Implementation;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehicleDashboard.VehicleConnection.Domain.Services.Implementation
{
   public class CustomerVehicleHistoryService : ICustomerVehicleHistoryService
    {
        private readonly VehicleConnectionHistoryDataContext _dbContext;
        private ICustomerVehicleHistoryRepository _customerVehicleHistoryRepo;
        private readonly ILogger<CustomerVehicleHistoryService> _logger;


        public CustomerVehicleHistoryService(VehicleConnectionHistoryDataContext dbContext,
            ILogger<CustomerVehicleHistoryService> logger )
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        #region Properties
        public ICustomerVehicleHistoryRepository customerVehiclesHistoryRepo
        {
            get
            {
                if (_customerVehicleHistoryRepo == null)
                {
                    _customerVehicleHistoryRepo = new CustomerVehicleHistoryRepository(_dbContext);
                }
                return _customerVehicleHistoryRepo;
            }
        }
        #endregion

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async  Task AddCustomerVehicleHistory(CustomerVehicleHistoryDTO customerVehicleHistoryDto)
        {
            try
            {
                var CustomerVehicleHistoryEntity = customerVehicleHistoryDto.GetEntity();
                customerVehiclesHistoryRepo.Add(CustomerVehicleHistoryEntity);
                await customerVehiclesHistoryRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public  ResponseModel<IEnumerable<CustomerVehicleHistoryDTO>> GetCustomerVehicleHistory(string vehicleId, int customerId, string regNo)
        {
            ResponseModel<IEnumerable<CustomerVehicleHistoryDTO>> returnResponse = new ResponseModel<IEnumerable<CustomerVehicleHistoryDTO>>();
            List<CustomerVehicleHistoryDTO> customerVehicleHistoryListDTO = new List<CustomerVehicleHistoryDTO>();
            try
            {
                IQueryable<CustomerVehicleHistory> customerVehicleHistory = customerVehiclesHistoryRepo.GetGustomerVehicleHistory(customerId, vehicleId, regNo);
                customerVehicleHistoryListDTO = CustomerVehicleHistoryDTO.MapFields(customerVehicleHistory);
                returnResponse.Entity = customerVehicleHistoryListDTO;
                returnResponse.TotalRows = customerVehicleHistoryListDTO.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }
            return returnResponse;
        }
    }
}
