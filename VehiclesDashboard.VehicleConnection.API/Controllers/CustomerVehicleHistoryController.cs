﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleDashboard.Core.Common.Models;
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
        public  IActionResult Get(string vehicleId,int customerId,string regNo)
        {
            ResponseModel<IEnumerable<CustomerVehicleHistoryDTO>> customerVehicleHistoryResponse = new ResponseModel<IEnumerable<CustomerVehicleHistoryDTO>>();
            try
            {
                customerVehicleHistoryResponse= _vehiclesHistoryService.GetCustomerVehicleHistory(vehicleId, customerId, regNo);
                if (customerVehicleHistoryResponse.ReturnStatus)
                    return Ok(customerVehicleHistoryResponse);
                else
                    return BadRequest(customerVehicleHistoryResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
    }
}
