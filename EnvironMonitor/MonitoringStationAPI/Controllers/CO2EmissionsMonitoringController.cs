using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using System.Linq;

namespace CO2EmissionsMonitoringController.Controllers
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
                return BadRequest("Invalid CO2 emission data");
            }

            // Save CO2 emission data to the database
            _dbContext.Sensor.Add(co2EmissionsData);
            _dbContext.SaveChanges();

            return Ok(co2EmissionsData);
        }

        // Added method
        [HttpGet("{id}")]
        public IActionResult GetCO2EmissionById(int id)
        {
            var co2EmissionData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id);
            if (co2EmissionData == null)
            {
                return NotFound("CO2 emission data not found");
            }

            return Ok(co2EmissionData);
        }

        // Added method
        [HttpGet]
        public IActionResult GetAllCO2Emission()
        {
            var allCO2EmissionData = _dbContext.Sensor.ToList();
            if (!allCO2EmissionData.Any())
            {
                return NotFound("No CO2 emission data found");
            }

            return Ok(allCO2EmissionData);
        }
    }
}
