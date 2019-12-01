using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using VehicleDashboard.VehicleConnection.Domain.Helpers;
using VehicleDashboard.VehicleConnection.Domain.Model;
using VehicleDashboard.VehicleConnection.Domain.Repositories;
using VehicleDashboard.VehicleConnection.Domain.Repositories.Implementation;
using VehicleDashboard.VehicleConnection.Domain.Services;
using VehicleDashboard.VehicleConnection.Domain.Services.Implementation;
using VehicleDashboard.VehicleConnection.Domain.UnitTest.Helper;
using VehicleDashboard.VehicleConnection.DTO;
using Xunit;

namespace VehicleDashboard.VehicleConnection.Domain.UnitTest
{
    public class CustomerVehicleHistoryServiceTest : IClassFixture<VehicleConnectionDomainClassInitializationFixture>
    {
        public Mock<ICustomerVehicleHistoryRepository> _customerVehicleHistoryRepoMock { get; set; }
        public CustomerVehicleHistoryService _customerVehicleHistoryServiceMock { get; set; }
        public List<CustomerVehicleHistoryDTO> _customerVehicleHistoryDto { get; set; }


        public CustomerVehicleHistoryServiceTest(VehicleConnectionDomainClassInitializationFixture fixture)
        {
            _customerVehicleHistoryRepoMock = fixture.customerVehicleHistoryRepoMock;
            _customerVehicleHistoryServiceMock = fixture.customerVehicleHistoryServiceMock;
            _customerVehicleHistoryDto = fixture._customerVehicleHistoryDto;
        }
        [Fact]
        public async void  Add_ReturnsSaveSuccessResult_WhenCustomerVehiclelIsValid()
        {
            // Arrange
            var customerVehicleHistoryDTOToAdd = new CustomerVehicleHistoryDTO() { VIN = _customerVehicleHistoryDto[0].VIN , ConnectionStatus = false,
                    ModificationStatus = DateTime.Now,CustomerName= _customerVehicleHistoryDto[0].CustomerName,CustomerId=_customerVehicleHistoryDto[0].CustomerId,RegNo= _customerVehicleHistoryDto[0].RegNo
            };
            
            _customerVehicleHistoryRepoMock.Setup(rep => rep.SaveChangesAsync()).Returns(() => Task<int>.Run(() => { return 1; }));

            //Act
            try
            {
                await _customerVehicleHistoryServiceMock.AddCustomerVehicleHistory(customerVehicleHistoryDTOToAdd);
            }
            catch (Exception ex)
            {
                Assert.False(true,ex.Message);
            }
        }

      
        [Fact]
        public async void Add_ReturnsException_WhnNullDTOPassAsInput()
        {
            // Act
            try
            {
                await _customerVehicleHistoryServiceMock.AddCustomerVehicleHistory(null);
            }
            catch (Exception ex) {

                Assert.Contains("Value cannot be null.", ex.Message);
            }

        }

    }
}
