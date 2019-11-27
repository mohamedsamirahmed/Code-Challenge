using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VehicleDashboard.VehicleConnection.Data;
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
        
    }
}
