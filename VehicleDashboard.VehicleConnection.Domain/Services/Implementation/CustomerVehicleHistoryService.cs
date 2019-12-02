using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.EventBusRabbitMQ.Events;
using VehicleDashboard.VehicleConnection.Data;
using VehicleDashboard.VehicleConnection.Domain.Helpers;
using VehicleDashboard.VehicleConnection.Domain.Mapper_Configuration;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.Domain.Repositories;
using VehicleDashboard.VehicleConnection.Domain.Repositories.Implementation;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehicleDashboard.VehicleConnection.Domain.Services.Implementation
{
   public class CustomerVehicleHistoryService : ICustomerVehicleHistoryService
    {
        #region Property Declaration
        private readonly VehicleConnectionHistoryDataContext _dbContext;
        private ICustomerVehicleHistoryRepository _customerVehicleHistoryRepo;
        private readonly ILogger<CustomerVehicleHistoryService> _logger;
        #endregion

        #region Constructor
        public CustomerVehicleHistoryService(VehicleConnectionHistoryDataContext dbContext,
            ILogger<CustomerVehicleHistoryService> logger )
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        #endregion

        #region Repositories Property
        public ICustomerVehicleHistoryRepository customerVehicleHistoryRepo
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

        #region Disposal object
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        #endregion


        #region public Operations
        /// <summary>
        /// Add RabbitMq event message into customer vehicle history table
        /// </summary>
        /// <param name="customerVehicleHistoryDto"></param>
        /// <returns></returns>
        //public async Task AddCustomerVehicleHistory(CustomerVehicleHistoryDTO customerVehicleHistoryDto)
        public async  Task AddCustomerVehicleHistory(CustomerVehicleChangedIntegrationEvent customerVehicleHistoryEventMessage)
        {
            try
            {
                Utility customerVehicleHelper = new Utility();

                var CustomerVehicleHistoryEntity = customerVehicleHelper.GetCustomerVehicleHistoryEntity(customerVehicleHistoryEventMessage);
                
                customerVehicleHistoryRepo.Add(CustomerVehicleHistoryEntity);
              int res=  await customerVehicleHistoryRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        //public async  Task<PagedList<CustomerVehicleHistoryDTO>> GetCustomerVehicleHistory(string vehicleId, int customerId, string regNo, CustomerVehicleHistoryParams customerVehicleHistoryParams)
        public async Task<ResponseModel<PagedList<CustomerVehicleHistoryDTO>>> GetCustomerVehicleHistory(string vehicleId, int customerId, string regNo, CustomerVehicleHistoryParams customerVehicleHistoryParams)
        {
            ResponseModel<PagedList<CustomerVehicleHistoryDTO>> returnResponse = new ResponseModel<PagedList<CustomerVehicleHistoryDTO>>();
            PagedList<CustomerVehicleHistoryDTO> pagedCustomerVehicleHistoryDto = null;
            try
            {
                IQueryable<CustomerVehicleHistory> customerVehicleHistory = customerVehicleHistoryRepo.GetGustomerVehicleHistory(customerId, vehicleId, regNo);
                var pagedCustomerVehicleHistory = await PagedList<CustomerVehicleHistory>.CreateAsync(customerVehicleHistory, customerVehicleHistoryParams.PageNumber, customerVehicleHistoryParams.PageSize);
                pagedCustomerVehicleHistoryDto = Mapping.Mapper.Map<PagedList<CustomerVehicleHistory>, PagedList<CustomerVehicleHistoryDTO>>(pagedCustomerVehicleHistory);
                returnResponse.Entity = pagedCustomerVehicleHistoryDto;
                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }
            return returnResponse;
        }

        #endregion

    }
}
