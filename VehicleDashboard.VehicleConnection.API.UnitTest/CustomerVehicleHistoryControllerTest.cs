using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.VehicleConnection.Domain.Helpers;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.Domain.Services;
using VehicleDashboard.VehicleConnection.DTO;
using VehiclesDashboard.VehicleConnection.API.Controllers;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.VehicleConnection.API.UnitTest.Helper;

namespace VehicleDashboard.VehicleConnection.API.UnitTest
{
    public class CustomerVehicleHistoryControllerTest : IClassFixture<VehicleHistoryAPIClassInitializationFixture>
    {
        public List<CustomerVehicleHistoryDTO> _filteredCustomerVehicleHistoryDto { get; set; }
        public Mock<ICustomerVehicleHistoryService> customerVehicleHistoryServiceMock { get; set; }
        private CustomerVehicleHistoryParams customerVehicleHistoryParams = null;
        private CustomerVehicleHistoryController customerVehicleHistorycontroller = null;


        public CustomerVehicleHistoryControllerTest(VehicleHistoryAPIClassInitializationFixture fixture)
        {
            _filteredCustomerVehicleHistoryDto = fixture._filteredCustomerVehicleHistoryDto;
            customerVehicleHistoryServiceMock = fixture.customerVehicleHistoryServiceMock;
            customerVehicleHistoryParams = fixture.customerVehicleHistoryParams;
            customerVehicleHistorycontroller = fixture.customerVehicleHistorycontroller;
        }


        [Fact]
        public async void GetCustomersVehicleHistory_ReturnsOKActionResult_WithListOfCustomerVehicleHistoryDTO()
        {
            // Arrange
            var pagedFilteredCustomerVehicleHistory = new PagedList<CustomerVehicleHistoryDTO>(_filteredCustomerVehicleHistoryDto, _filteredCustomerVehicleHistoryDto.Count(), customerVehicleHistoryParams.PageNumber, customerVehicleHistoryParams.PageSize);
            var responseModel = new ResponseModel<PagedList<CustomerVehicleHistoryDTO>>() { Entity = pagedFilteredCustomerVehicleHistory, ReturnStatus = true };
            customerVehicleHistoryServiceMock.Setup(service => service.GetCustomerVehicleHistory("YS2R4X20005399401", 1, "ABC123", customerVehicleHistoryParams)).ReturnsAsync(responseModel);

            // Act
            var controllerResult = await customerVehicleHistorycontroller.Get("YS2R4X20005399401", 1, "ABC123", customerVehicleHistoryParams);

            // Assert
            Assert.IsType<OkObjectResult>(controllerResult);
            var customerVehicleHistoryList = controllerResult as OkObjectResult;
            var list = customerVehicleHistoryList.Value as ResponseModel<PagedList<CustomerVehicleHistoryDTO>>;
            Assert.Equal(3, list.Entity.Count());
        }


        [Fact]
        public async void GetCustomersVehicleHistory_ReturnsBadResponse_WithResponseStatusMessage()
        {
            // Arrange
            customerVehicleHistoryServiceMock.Setup(service => service.GetCustomerVehicleHistory("YS2R4X20005399401", 1, "ABC123", customerVehicleHistoryParams)).Throws<Exception>();

            // Act
            var controllerResult = await customerVehicleHistorycontroller.Get("YS2R4X20005399401", 1, "ABC123", customerVehicleHistoryParams);

            // Assert
            Assert.IsType<BadRequestObjectResult>(controllerResult);
            var customerVehicleHistoryResult = controllerResult as BadRequestObjectResult;
            var responseResult = customerVehicleHistoryResult.Value;
            Assert.Equal("Something wrong happened!. Please try again later.", responseResult);
        }



    

    }


}
