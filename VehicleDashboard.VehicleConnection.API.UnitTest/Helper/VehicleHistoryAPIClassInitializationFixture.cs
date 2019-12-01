using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleDashboard.VehicleConnection.Domain.Helpers;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.Domain.Services;
using VehicleDashboard.VehicleConnection.DTO;
using VehiclesDashboard.VehicleConnection.API.Controllers;

namespace VehicleDashboard.VehicleConnection.API.UnitTest.Helper
{
    public class VehicleHistoryAPIClassInitializationFixture
    {

        public List<CustomerVehicleHistoryDTO> _customerVehicleHistoryDto { get; set; }
        public List<CustomerVehicleHistoryDTO> _filteredCustomerVehicleHistoryDto { get; set; }
        public List<CustomerVehicleHistory> _filteredCustomerVehicleHistory { get; set; }


        public Mock<ICustomerVehicleHistoryService> customerVehicleHistoryServiceMock { get; set; }
        public CustomerVehicleHistoryParams customerVehicleHistoryParams = null;
        public CustomerVehicleHistoryController customerVehicleHistorycontroller = null;
        public VehicleHistoryAPIClassInitializationFixture()
        {
            GetFakeData();

            customerVehicleHistoryServiceMock = new Mock<ICustomerVehicleHistoryService>();
            customerVehicleHistoryParams = new CustomerVehicleHistoryParams();
            ConfigureCustomerVehicleHistoryController();
        }

        #region Helper 
        private void ConfigureCustomerVehicleHistoryController()
        {
            var _loggerMock= Mock.Of<ILogger<CustomerVehicleHistoryController>>();
            customerVehicleHistorycontroller = new CustomerVehicleHistoryController(customerVehicleHistoryServiceMock.Object, _loggerMock);

            // Ensure the controller can add response headers
            customerVehicleHistorycontroller.ControllerContext = new ControllerContext();
            customerVehicleHistorycontroller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

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

        #endregion
    }
}
