using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace AirPollutionMonitoringController.Controllers
{
    [Route("api/co2-emissions-monitoring-station")]
    [ApiController]
    public class CO2EmissionsMonitoringController : ControllerBase
    {
        private readonly EnvironmentalDataContext _dbContext;

        public CO2EmissionsMonitoringController(EnvironmentalDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult PostCO2EmissionsData([FromBody] Sensor co2EmissionsData)
        {
            if (co2EmissionsData == null)
            {
                return BadRequest("Invalid humdity data");
            }

            // Save temperature data to the database
            _dbContext.Sensor.Add(co2EmissionsData);
            _dbContext.SaveChanges();

            return Ok(co2EmissionsData);
        }
        
    }
}
