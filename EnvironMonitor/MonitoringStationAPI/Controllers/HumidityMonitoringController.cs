using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using System.Linq;

namespace HumidityMonitoringController.Controllers
{
    [Route("api/humidity-monitoring-station")]
    [ApiController]
    public class HumidityMonitoringController : ControllerBase
    {
        private readonly EnvironmentalDataContext _dbContext;

        public HumidityMonitoringController(EnvironmentalDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult PostHumidityData([FromBody] Sensor humidityData)
        {
            if (humidityData == null)
            {
                return BadRequest("Invalid humidity data");
            }

            _dbContext.Sensor.Add(humidityData);
            _dbContext.SaveChanges();

            return Ok(humidityData);
        }

        [HttpPut("{id}")]
        public IActionResult PutHumidityData(int id, [FromBody] Sensor updatedHumidityData)
        {
            var humidityData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (humidityData == null)
            {
                return NotFound();
            }

            if (updatedHumidityData == null || id != updatedHumidityData.Id) // Changed 'id' to 'Id'
            {
                return BadRequest();
            }

            humidityData.ParameterValue = updatedHumidityData.ParameterValue;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetHumidityData(int id)
        {
            var humidityData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (humidityData == null)
            {
                return NotFound();
            }

            return Ok(humidityData);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHumidityData(int id)
        {
            var humidityData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (humidityData == null)
            {
                return NotFound();
            }

            _dbContext.Sensor.Remove(humidityData);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
