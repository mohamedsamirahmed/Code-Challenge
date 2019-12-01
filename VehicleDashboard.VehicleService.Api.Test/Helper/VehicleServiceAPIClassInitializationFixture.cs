using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleDashboard.VehicleService.Domain.Helpers;
using VehicleDashboard.VehicleService.Domain.Services;
using VehicleDashboard.VehicleService.DTO;
using VehiclesDashboard.VehicleServices.API.Controllers;

namespace VehicleDashboard.VehicleService.Api.Test.Helper
{
    public class VehicleServiceAPIClassInitializationFixture
    {

        public List<CustomerVehiclesDTO> _customerVehicleDashboardDto { get; set; }
        public List<CustomerVehiclesDTO> _filteredCustomerVehicleDashboardDto { get; set; }

        public List<LookupDTO> _customersDtoLookupLst { get; set; }
        public List<LookupDTO> _vehiclesDtoLookupLst { get; set; }
        public Mock<IVehicleDashboardService> customerVehicleDashboardServiceMock { get; set; }
        public CustomerVehicleParams customerVehicleDashboardParams = null;
        public CustomerVehiclesController customerVehicleDashboardcontroller = null;
        public VehicleServiceAPIClassInitializationFixture()
        {
            GetFakeData();

            customerVehicleDashboardServiceMock = new Mock<IVehicleDashboardService>();
            customerVehicleDashboardParams = new CustomerVehicleParams();
            ConfigureCustomerVehicleHistoryController();
        }

        #region Helper 
        private void ConfigureCustomerVehicleHistoryController()
        {
            var _loggerMock = Mock.Of<ILogger<CustomerVehiclesController>>();
            customerVehicleDashboardcontroller = new CustomerVehiclesController(customerVehicleDashboardServiceMock.Object, _loggerMock);

            // Ensure the controller can add response headers
            customerVehicleDashboardcontroller.ControllerContext = new ControllerContext();
            customerVehicleDashboardcontroller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        private void GetFakeData()
        {
            _filteredCustomerVehicleDashboardDto = new List<CustomerVehiclesDTO>() {
                    new CustomerVehiclesDTO(){ customerId = 1,VIN = "YS2R4X20005399401", RegNo = "ABC123", IsConnectedStatus = true, LastModificationStatus = DateTime.Now},
                new CustomerVehiclesDTO(){ customerId = 1,VIN = "YS2R4X20005399401", RegNo = "ABC123", IsConnectedStatus = false, LastModificationStatus = DateTime.Now},
            };

            
            _customerVehicleDashboardDto = new List<CustomerVehiclesDTO>();
            _customersDtoLookupLst = new List<LookupDTO>();
            _vehiclesDtoLookupLst = new List<LookupDTO>();

            _customerVehicleDashboardDto.AddRange(_filteredCustomerVehicleDashboardDto);
            _customerVehicleDashboardDto.AddRange(new List<CustomerVehiclesDTO>() {
                new CustomerVehiclesDTO(){ customerId = 1,VIN = "VLUR4X20009093588", RegNo = "GHI789", IsConnectedStatus = true, LastModificationStatus = DateTime.Now},
                new CustomerVehiclesDTO(){ customerId = 2,VIN = "YS2R4X20005388011", RegNo = "JKL012", IsConnectedStatus = false, LastModificationStatus = DateTime.Now},
                new CustomerVehiclesDTO(){ customerId = 2,VIN = "YS2R4X20005387949", RegNo = "MNO345", IsConnectedStatus = true, LastModificationStatus = DateTime.Now},
                new CustomerVehiclesDTO(){ customerId = 3,VIN = "VLUR4X20009048066", RegNo = "PQR678", IsConnectedStatus = true, LastModificationStatus = DateTime.Now},
                new CustomerVehiclesDTO(){ customerId = 3,VIN = "YS2R4X20005387055", RegNo = "STU901", IsConnectedStatus = true, LastModificationStatus = DateTime.Now},
            });

            _customersDtoLookupLst.AddRange(new List<LookupDTO>(){new LookupDTO() {text = "Kalles Grustransporter AB",value="1"},
                new LookupDTO(){text = "Johans Bulk AB",value = "2"},
                new LookupDTO(){text = "Haralds Värdetransporter AB",value = "3"} });

            _vehiclesDtoLookupLst.AddRange(new List<LookupDTO>(){new LookupDTO() {text = "YS2R4X20005399401",value="YS2R4X20005399401"},
                new LookupDTO(){text = "YS2R4X20005399401",value = "YS2R4X20005399401"},
                new LookupDTO(){text = "VLUR4X20009093588",value = "VLUR4X20009093588"},
                new LookupDTO(){text = "YS2R4X20005388011",value = "YS2R4X20005388011"},
                new LookupDTO(){text = "YS2R4X20005387949",value = "YS2R4X20005387949"},
                new LookupDTO(){text = "VLUR4X20009048066",value = "VLUR4X20009048066"},
                new LookupDTO(){text = "YS2R4X20005387055",value = "YS2R4X20005387055"},
            });
        }

        #endregion
    }
}
