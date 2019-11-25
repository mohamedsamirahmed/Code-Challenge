using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.VehicleConnection.Domain.Services;
using VehicleDashboard.VehicleConnection.DTO;
using VehiclesDashboard.VehicleConnection.API.IntegrationEvents;

namespace VehiclesDashboard.VehicleConnection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerVehicleHistoryController : ControllerBase
    {
        private ICustomerVehicleHistoryService _vehiclesDashboardService;
        private readonly ILogger<CustomerVehicleHistoryController> _logger;
        private readonly ICustomerVehicleHistoryIntegrationEventService _customerVehicleHistoryIntegrationEventService;

        public CustomerVehicleHistoryController(ICustomerVehicleHistoryService vehiclesDashboardService, 
            ILogger<CustomerVehicleHistoryController> logger,
            ICustomerVehicleHistoryIntegrationEventService customerVehicleHistoryIntegrationEventService)
        {
            _vehiclesDashboardService = vehiclesDashboardService;
            _logger = logger;
            _customerVehicleHistoryIntegrationEventService = customerVehicleHistoryIntegrationEventService;
        }

        // GET api/CustomerVehicles
        [HttpGet()]
        public IEnumerable<string> Get()
        {
            return new string[] { "value" };
        }


        //POST api/v1/[controller]/items
        [HttpPost]
        [Route("CreateCustomerVehicleHistory")]
        public async Task<IActionResult> CreateCustomerVehicleHistory([FromBody]CustomerVehicleHistoryDTO customerVehicleHistoryDto)
        {
            try
            {
                if (!ModelState.IsValid) {
                    _logger.LogError("Invalid Input!");
                    return BadRequest(ModelState);
                }
                //Create Integration Event to be published through the Event Bus
                var customerVehicleHistoryChangedEvent = new CustomerVehicleChangedIntegrationEvent(customerVehicleHistoryDto);

                // Publish through the Event Bus and mark the saved event as published
                await _customerVehicleHistoryIntegrationEventService.PublishThroughEventBusAsync(customerVehicleHistoryChangedEvent);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new InvalidOperationException("Something wrong happened!. Please try again later.");
            }
        }
    }
}
