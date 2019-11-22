using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.Shared.Common.Models;
using VehicleDashboard.VehicleService.Domain.Services;
using VehicleDashboard.VehicleService.DTO;

namespace VehiclesDashboard.VehicleServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerVehiclesController : ControllerBase
    {

        private IVehicleDashboardService _vehiclesDashboardService;
        private readonly ILogger<CustomerVehiclesController> _logger;
        public CustomerVehiclesController(IVehicleDashboardService vehiclesDashboardService, ILogger<CustomerVehiclesController> logger)
        {
            _vehiclesDashboardService = vehiclesDashboardService;
            _logger = logger;
        }


        /// <summary>
        /// get all current customers Vehicles 
        /// </summary>
        /// <returns>customer vehicles response status including collection of customer vehicles.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            ResponseModel<IEnumerable<CustomerVehiclesDTO>> customerVehiclesResponse = new ResponseModel<IEnumerable<CustomerVehiclesDTO>>();
            try
            {
                customerVehiclesResponse = _vehiclesDashboardService.GetCustomerVehicleList();
                if (customerVehiclesResponse.ReturnStatus)
                    return Ok(customerVehiclesResponse);
                else
                    return BadRequest(customerVehiclesResponse);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new InvalidOperationException("Something wrong happened!. Please try again later.");
            }

        }

        // GET api/CustomerVehicles/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/CustomerVehicles
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/CustomerVehicles/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/CustomerVehicles/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
