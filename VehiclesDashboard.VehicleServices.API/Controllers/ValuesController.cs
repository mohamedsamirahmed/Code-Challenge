using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VehicleDashboard.VehicleService.Domain.Services;

namespace VehiclesDashboard.VehicleServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private IVehicleDashboardService _vehiclesDashboardService;
        public ValuesController(IVehicleDashboardService vehiclesDashboardService)
        {
            _vehiclesDashboardService = vehiclesDashboardService;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vals = await  _vehiclesDashboardService.GetCustomers().AsQueryable().ToListAsync();
            return Ok(vals);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
