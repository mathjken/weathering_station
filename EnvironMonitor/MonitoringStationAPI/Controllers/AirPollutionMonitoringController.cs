using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;

namespace AirPollutionMonitoringController.Controllers
{
    [Route("api/air-pollution-monitoring-station")]
    [ApiController]
    public class AirPollutionMonitoringController : ControllerBase
    {
        private readonly EnvironmentalDataContext _dbContext;

        public AirPollutionMonitoringController(EnvironmentalDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult PostAirPollutionData([FromBody] Sensor airPollutionData)
        {
            if (airPollutionData == null)
            {
                return BadRequest("Invalid humdity data");
            }

            // Save temperature data to the database
            _dbContext.Sensor.Add(airPollutionData);
            _dbContext.SaveChanges();

            return Ok(airPollutionData);
        }
        
    }
}
