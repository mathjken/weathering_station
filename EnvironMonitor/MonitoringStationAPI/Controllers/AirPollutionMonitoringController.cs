using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using System.Linq;

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
                return BadRequest("Invalid air pollution data");
            }

            _dbContext.Sensor.Add(airPollutionData);
            _dbContext.SaveChanges();

            return Ok(airPollutionData);
        }

        [HttpGet("{id}")]
        public IActionResult GetAirPollutionById(int id)
        {
            var airPollutionData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id);
            if (airPollutionData == null)
            {
                return NotFound("Air pollution data not found");
            }

            return Ok(airPollutionData);
        }

        [HttpGet]
        public IActionResult GetAllAirPollution()
        {
            var allAirPollutionData = _dbContext.Sensor.ToList();
            if (!allAirPollutionData.Any())
            {
                return NotFound("No air pollution data found");
            }

            return Ok(allAirPollutionData);
        }

        // Added method: Delete air pollution data by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteAirPollutionData(int id)
        {
            var airPollutionData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id);
            if (airPollutionData == null)
            {
                return NotFound("Air pollution data not found");
            }

            _dbContext.Sensor.Remove(airPollutionData);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // Added method: Update air pollution data by ID
        [HttpPut("{id}")]
        public IActionResult UpdateAirPollutionData(int id, [FromBody] Sensor updatedAirPollutionData)
        {
            if (updatedAirPollutionData == null || id != updatedAirPollutionData.Id)
            {
                return BadRequest("Invalid request");
            }

            var airPollutionData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id);
            if (airPollutionData == null)
            {
                return NotFound("Air pollution data not found");
            }

            // Update relevant properties based on your Sensor model definition
            airPollutionData.Parameter = updatedAirPollutionData.Parameter;
            airPollutionData.Unit = updatedAirPollutionData.Unit;
            airPollutionData.ParameterValue = updatedAirPollutionData.ParameterValue;
            airPollutionData.TimeStamp = updatedAirPollutionData.TimeStamp;
            airPollutionData.Warning = updatedAirPollutionData.Warning;
            airPollutionData.DataCollectionInterval = updatedAirPollutionData.DataCollectionInterval;
            // ... Update other properties as needed ... (remove DataRangeMin and DataRangeMax)

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
