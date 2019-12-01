using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.VehicleConnection.Data;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.Domain.Repositories;
using VehicleDashboard.VehicleConnection.Domain.Services.Implementation;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehicleDashboard.VehicleConnection.Domain.UnitTest.Helper
{
    public class VehicleConnectionDomainClassInitializationFixture
    {

        public List<CustomerVehicleHistoryDTO> _customerVehicleHistoryDto { get; set; }
        public List<CustomerVehicleHistoryDTO> _filteredCustomerVehicleHistoryDto { get; set; }
        public List<CustomerVehicleHistory> _filteredCustomerVehicleHistory { get; set; }


        public Mock<ICustomerVehicleHistoryRepository> customerVehicleHistoryRepoMock { get; set; }

        public CustomerVehicleHistoryService customerVehicleHistoryServiceMock = null;

        public  VehicleConnectionDomainClassInitializationFixture()
        {
            var _loggerMock = Mock.Of<ILogger<CustomerVehicleHistoryService>>();

            GetFakeData();
            var dbContext =  GetDatabaseContext();
            customerVehicleHistoryRepoMock = new Mock<ICustomerVehicleHistoryRepository>();
            customerVehicleHistoryServiceMock = new CustomerVehicleHistoryService(dbContext, _loggerMock);
        }



        private  VehicleConnectionHistoryDataContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<VehicleConnectionHistoryDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new VehicleConnectionHistoryDataContext(options);
            databaseContext.Database.EnsureCreated();
            if ( databaseContext.CustomerehicleHistory.Count() <= 0)
            {
                foreach (var customerVehicleHistoryFakeData in GetFakeCustomerVehicleHistoryData())
                {
                    databaseContext.CustomerehicleHistory.Add(customerVehicleHistoryFakeData);
                     databaseContext.SaveChanges();
                } 
            }
            return databaseContext;
        }


        #region Helper 
        private void GetFakeData()
        {
            _filteredCustomerVehicleHistoryDto = new List<CustomerVehicleHistoryDTO>() {
                    new CustomerVehicleHistoryDTO(){ CustomerId = 1,VIN = "YS2R4X20005399401", RegNo = "ABC123", ConnectionStatus = true, ModificationStatus = DateTime.Now,CustomerName="Kalles Grustransporter AB" },
                new CustomerVehicleHistoryDTO(){ CustomerId = 1,VIN = "YS2R4X20005399401", RegNo = "ABC123", ConnectionStatus = false, ModificationStatus = DateTime.Now,CustomerName="Kalles Grustransporter AB" },
                new CustomerVehicleHistoryDTO(){ CustomerId = 1,VIN = "YS2R4X20005399401", RegNo = "ABC123", ConnectionStatus = true, ModificationStatus = DateTime.Now,CustomerName="Kalles Grustransporter AB" }
            };

            _filteredCustomerVehicleHistory = new List<CustomerVehicleHistory>() {
                    new CustomerVehicleHistory(){ CustomerId = 1,VehicleId = "YS2R4X20005399401", RegNo = "ABC123", ConnectionStatus = true, StatusModificationTime = DateTime.Now,CustomerName="Kalles Grustransporter AB" },
                new CustomerVehicleHistory(){ CustomerId = 1,VehicleId = "YS2R4X20005399401", RegNo = "ABC123", ConnectionStatus = false, StatusModificationTime = DateTime.Now,CustomerName="Kalles Grustransporter AB" },
                new CustomerVehicleHistory(){ CustomerId = 1,VehicleId = "YS2R4X20005399401", RegNo = "ABC123", ConnectionStatus = true, StatusModificationTime = DateTime.Now,CustomerName="Kalles Grustransporter AB" }
            };

            _customerVehicleHistoryDto = new List<CustomerVehicleHistoryDTO>();

            _customerVehicleHistoryDto.AddRange(_filteredCustomerVehicleHistoryDto);
            _customerVehicleHistoryDto.AddRange(new List<CustomerVehicleHistoryDTO>() {
                new CustomerVehicleHistoryDTO(){ CustomerId = 1,VIN = "VLUR4X20009093588", RegNo = "GHI789", ConnectionStatus = true, ModificationStatus = DateTime.Now,CustomerName="Kalles Grustransporter AB" },
                new CustomerVehicleHistoryDTO(){ CustomerId = 2,VIN = "YS2R4X20005388011", RegNo = "JKL012", ConnectionStatus = false, ModificationStatus = DateTime.Now,CustomerName="Johans Bulk AB" },
                new CustomerVehicleHistoryDTO(){ CustomerId = 2,VIN = "YS2R4X20005387949", RegNo = "MNO345", ConnectionStatus = true, ModificationStatus = DateTime.Now,CustomerName="Johans Bulk AB" },
                new CustomerVehicleHistoryDTO(){ CustomerId = 2,VIN = "YS2R4X20005387949", RegNo = "MNO345", ConnectionStatus = false, ModificationStatus = DateTime.Now,CustomerName="Johans Bulk AB" },
                new CustomerVehicleHistoryDTO(){ CustomerId = 3,VIN = "VLUR4X20009048066", RegNo = "PQR678", ConnectionStatus = true, ModificationStatus = DateTime.Now,CustomerName="Haralds Värdetransporter AB" },
                new CustomerVehicleHistoryDTO(){ CustomerId = 3,VIN = "YS2R4X20005387055", RegNo = "STU901", ConnectionStatus = true, ModificationStatus = DateTime.Now,CustomerName="Haralds Värdetransporter AB" },
            });

        }

        private List<CustomerVehicleHistory> GetFakeCustomerVehicleHistoryData()
        {
            List<CustomerVehicleHistory> _customerVehicleHistory = new List<CustomerVehicleHistory>();
            foreach (CustomerVehicleHistoryDTO customerVehicleHistoryDto in _customerVehicleHistoryDto)
            {
                _customerVehicleHistory.Add(new CustomerVehicleHistory()
                {
                    ConnectionStatus = customerVehicleHistoryDto.ConnectionStatus,
                    CustomerId = customerVehicleHistoryDto.CustomerId,
                    CustomerName = customerVehicleHistoryDto.CustomerName,
                    RegNo = customerVehicleHistoryDto.RegNo,
                    StatusModificationTime = customerVehicleHistoryDto.ModificationStatus,
                    VehicleId = customerVehicleHistoryDto.VIN
                });

            }
            return _customerVehicleHistory;
        }


        #endregion
    }
}
