using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.Core.Common.Helper;
using VehicleDashboard.Core.Common.Models;
using VehicleDashboard.VehicleService.Domain.Helpers;
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
        public async Task<IActionResult> Get([FromQuery]CustomerVehicleParams customerVehicleParams)
        {
            //  ResponseModel<IEnumerable<CustomerVehiclesDTO>> customerVehiclesResponse = new ResponseModel<IEnumerable<CustomerVehiclesDTO>>();
            try
            {
                //customerVehiclesResponse = _vehiclesDashboardService.GetCustomerVehicleList(customerVehicleParams);
                var customerVehiclesResponse = await _vehiclesDashboardService.GetCustomerVehicleList(customerVehicleParams);

                Response.AddPagination(customerVehiclesResponse.CurrentPage, customerVehiclesResponse.PageSize,
                    customerVehiclesResponse.TotalCount, customerVehiclesResponse.TotalPages);

                return Ok(customerVehiclesResponse);
                //if (customerVehiclesResponse.ReturnStatus)
                //    return Ok(customerVehiclesResponse);
                //else
                //    return BadRequest(customerVehiclesResponse);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }


        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            try
            {
                ResponseModel<IQueryable<LookupDTO>> customerResponse = new ResponseModel<IQueryable<LookupDTO>>();
                customerResponse = _vehiclesDashboardService.GetCustomerLookup();
                if (!customerResponse.ReturnStatus)
                    return BadRequest(customerResponse);
                return Ok(customerResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }

        [HttpGet("GetVehicles")]
        public IActionResult GetVehicles()
        {
            try
            {
                ResponseModel<IQueryable<LookupDTO>> vehicleResponse = new ResponseModel<IQueryable<LookupDTO>>();
                vehicleResponse = _vehiclesDashboardService.GetVehicleLookup();
                if (!vehicleResponse.ReturnStatus)
                    return BadRequest(vehicleResponse);
                return Ok(vehicleResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }
    }
}
