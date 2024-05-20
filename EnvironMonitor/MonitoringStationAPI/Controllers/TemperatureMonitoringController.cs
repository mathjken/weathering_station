using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using System.Linq;

namespace TemperatureMonitoringController.Controllers
{
    [Route("api/temperature-monitoring-station")]
    [ApiController]
    public class TemperatureMonitoringController : ControllerBase
    {
        private readonly EnvironmentalDataContext _dbContext;

        public TemperatureMonitoringController(EnvironmentalDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult PostTemperatureData([FromBody] Sensor temperatureData)
        {
            if (temperatureData == null)
            {
                return BadRequest("Invalid temperature data");
            }

            _dbContext.Sensor.Add(temperatureData);
            _dbContext.SaveChanges();

            return Ok(temperatureData);
        }

        [HttpPut("{id}")]
        public IActionResult PutTemperatureData(int id, [FromBody] Sensor updatedTemperatureData)
        {
            var temperatureData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (temperatureData == null)
            {
                return NotFound();
            }

            if (updatedTemperatureData == null || id != updatedTemperatureData.Id) // Changed 'id' to 'Id'
            {
                return BadRequest();
            }

            temperatureData.ParameterValue = updatedTemperatureData.ParameterValue;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetTemperatureData(int id)
        {
            var temperatureData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id);
            if (temperatureData == null)
            {
                return NotFound();
            }

            return Ok(temperatureData);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTemperatureData(int id)
        {
            var temperatureData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (temperatureData == null)
            {
                return NotFound();
            }

            _dbContext.Sensor.Remove(temperatureData);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // Added method
        [HttpGet]
        public IActionResult GetAllTemperatureData()
        {
            var allTemperatureData = _dbContext.Sensor.ToList();
            if (!allTemperatureData.Any())
            {
                return NotFound("No temperature data found");
            }

            return Ok(allTemperatureData);
        }
    }
}
