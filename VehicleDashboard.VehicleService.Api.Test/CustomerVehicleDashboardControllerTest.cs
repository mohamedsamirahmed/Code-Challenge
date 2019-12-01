using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.VehicleService.Api.Test.Helper;
using VehicleDashboard.VehicleService.Domain.Helpers;
using VehicleDashboard.VehicleService.Domain.Services;
using VehicleDashboard.VehicleService.DTO;
using VehiclesDashboard.VehicleServices.API.Controllers;
using Xunit;

namespace VehicleDashboard.VehicleService.Api.Test
{
 
    public class CustomerVehicleDashboardControllerTest : IClassFixture<VehicleServiceAPIClassInitializationFixture>
    {
        public List<CustomerVehiclesDTO> _customerVehicleDashboardDto { get; set; }
        public List<LookupDTO> _customerLookupLstDto { get; set; }
        public List<LookupDTO> _vehiclesLookupLstDto { get; set; }
        public Mock<IVehicleDashboardService> _customerVehicleDashboardServiceMock { get; set; }
        private CustomerVehicleParams _customerVehicleDashboardParams = null;
        private CustomerVehiclesController _customerVehicleDashboardcontroller = null;


        public CustomerVehicleDashboardControllerTest(VehicleServiceAPIClassInitializationFixture fixture)
        {
            _customerVehicleDashboardDto = fixture._customerVehicleDashboardDto;
            _customerVehicleDashboardServiceMock = fixture.customerVehicleDashboardServiceMock;
            _customerVehicleDashboardParams = fixture.customerVehicleDashboardParams;
            _customerVehicleDashboardcontroller = fixture.customerVehicleDashboardcontroller;
            _customerLookupLstDto = fixture._customersDtoLookupLst;
            _vehiclesLookupLstDto = fixture._vehiclesDtoLookupLst;
        }


        [Fact]
        public async void GetCustomersVehicleDashboard_ReturnsOKActionResult_WithListOfCustomerVehicleDTO()
        {
            // Arrange
            var pagedFilteredCustomerVehicleHistory = new PagedList<CustomerVehiclesDTO>(_customerVehicleDashboardDto, _customerVehicleDashboardDto.Count, _customerVehicleDashboardParams.PageNumber, _customerVehicleDashboardParams.PageSize);
            var responseModel = new ResponseModel<PagedList<CustomerVehiclesDTO>>() { Entity = pagedFilteredCustomerVehicleHistory, ReturnStatus = true };
            _customerVehicleDashboardServiceMock.Setup(service => service.GetCustomerVehicleList(_customerVehicleDashboardParams)).ReturnsAsync(responseModel);

            // Act
            var controllerResult = await _customerVehicleDashboardcontroller.Get(_customerVehicleDashboardParams);

            // Assert
            Assert.IsType<OkObjectResult>(controllerResult);
            var customerVehicleHistoryList = controllerResult as OkObjectResult;
            var list = customerVehicleHistoryList.Value as ResponseModel<PagedList<CustomerVehiclesDTO>>;
            Assert.Equal(7, list.Entity.Count);
        }


        [Fact]
        public async void GetCustomersVehicleList_ReturnsBadResponse_WithResponseStatusMessage()
        {
            // Arrange
            _customerVehicleDashboardServiceMock.Setup(service => service.GetCustomerVehicleList(_customerVehicleDashboardParams)).Throws<Exception>();

            // Act
            var controllerResult = await _customerVehicleDashboardcontroller.Get(_customerVehicleDashboardParams);

            // Assert
            Assert.IsType<BadRequestObjectResult>(controllerResult);
            var customerVehicleHistoryResult = controllerResult as BadRequestObjectResult;
            var responseResult = customerVehicleHistoryResult.Value;
            Assert.Equal("Something wrong happened!. Please try again later.", responseResult);
        }


        [Fact]
        public async void GetCustomers_ReturnsOKActionResult_WithListOfCustomersDTO()
        {
            // Arrange
            var responseModel = new ResponseModel<IQueryable<LookupDTO>>() { Entity = _customerLookupLstDto.AsQueryable(), ReturnStatus = true };
            _customerVehicleDashboardServiceMock.Setup(service => service.GetCustomerLookup()).Returns(responseModel);

            // Act
            var controllerResult =  _customerVehicleDashboardcontroller.GetCustomers();

            // Assert
            Assert.IsType<OkObjectResult>(controllerResult);
            var customerVehicleHistoryList = controllerResult as OkObjectResult;
            var list = customerVehicleHistoryList.Value as ResponseModel<IQueryable<LookupDTO>>;
            Assert.Equal(3, list.Entity.Count());
        }

        [Fact]
        public async void GetVehicles_ReturnsOKActionResult_WithListOfVehiclesDTO()
        {
            // Arrange
            var responseModel = new ResponseModel<IQueryable<LookupDTO>>() { Entity = _vehiclesLookupLstDto.AsQueryable(), ReturnStatus = true };
            _customerVehicleDashboardServiceMock.Setup(service => service.GetVehicleLookup()).Returns(responseModel);

            // Act
            var controllerResult = _customerVehicleDashboardcontroller.GetVehicles();

            // Assert
            Assert.IsType<OkObjectResult>(controllerResult);
            var customerVehicleHistoryList = controllerResult as OkObjectResult;
            var list = customerVehicleHistoryList.Value as ResponseModel<IQueryable<LookupDTO>>;
            Assert.Equal(7, list.Entity.Count());
        }
    }
}
