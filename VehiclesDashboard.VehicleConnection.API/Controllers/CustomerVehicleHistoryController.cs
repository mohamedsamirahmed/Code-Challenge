using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.VehicleConnection.Domain.Helpers;
using VehicleDashboard.VehicleConnection.Domain.Services;
using VehicleDashboard.VehicleConnection.DTO;

namespace VehiclesDashboard.VehicleConnection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerVehicleHistoryController : ControllerBase
    {
        private ICustomerVehicleHistoryService _vehiclesHistoryService;
        private readonly ILogger<CustomerVehicleHistoryController> _logger;

        public CustomerVehicleHistoryController(ICustomerVehicleHistoryService vehiclesDashboardService,
            ILogger<CustomerVehicleHistoryController> logger)
        {
            _vehiclesHistoryService = vehiclesDashboardService;
            _logger = logger;
        }

        // GET api/CustomerVehicleHistory/{vehicleId}/{customerId}/{regNo}
        [HttpGet("{vehicleId}/{customerId}/{regNo}")]
        public async Task<IActionResult> Get(string vehicleId, int customerId, string regNo, [FromQuery]CustomerVehicleHistoryParams customerVehicleHistoryParams)
        {
            try
            {
                var customerVehicleHistoryResponse = await _vehiclesHistoryService.GetCustomerVehicleHistory(vehicleId, customerId, regNo, customerVehicleHistoryParams);

                Response.AddPagination(customerVehicleHistoryResponse.Entity.CurrentPage, customerVehicleHistoryResponse.Entity.PageSize,
                   customerVehicleHistoryResponse.Entity.TotalCount, customerVehicleHistoryResponse.Entity.TotalPages);

                if (customerVehicleHistoryResponse.ReturnStatus)
                    return Ok(customerVehicleHistoryResponse);
                else
                    return BadRequest(customerVehicleHistoryResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }

    }
}
