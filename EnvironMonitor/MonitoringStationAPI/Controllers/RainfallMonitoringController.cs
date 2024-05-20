using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using System.Linq;

namespace RainfallMonitoringController.Controllers
{
    [Route("api/rainfall-monitoring-station")]
    [ApiController]
    public class RainfallMonitoringController : ControllerBase
    {
        private readonly EnvironmentalDataContext _dbContext;

        public RainfallMonitoringController(EnvironmentalDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult PostRainfallData([FromBody] Sensor rainfallData)
        {
            if (rainfallData == null)
            {
                return BadRequest("Invalid rainfall data");
            }

            _dbContext.Sensor.Add(rainfallData);
            _dbContext.SaveChanges();

            return Ok(rainfallData);
        }

        [HttpPut("{id}")]
        public IActionResult PutRainfallData(int id, [FromBody] Sensor updatedRainfallData)
        {
            var rainfallData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (rainfallData == null)
            {
                return NotFound();
            }

            if (updatedRainfallData == null || id != updatedRainfallData.Id) // Changed 'id' to 'Id'
            {
                return BadRequest();
            }

            rainfallData.ParameterValue = updatedRainfallData.ParameterValue;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetRainfallData(int id)
        {
            var rainfallData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (rainfallData == null)
            {
                return NotFound();
            }

            return Ok(rainfallData);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRainfallData(int id)
        {
            var rainfallData = _dbContext.Sensor.FirstOrDefault(s => s.Id == id); // Changed 'id' to 'Id'
            if (rainfallData == null)
            {
                return NotFound();
            }

            _dbContext.Sensor.Remove(rainfallData);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // Added method
        [HttpGet("all")]
        public IActionResult GetAllRainfall()
        {
            // Assuming 'Rainfall' is a property in the Sensor model
            var allRainfallData = _dbContext.Sensor.Select(s => s.Rainfall).ToList();

            if (!allRainfallData.Any())
            {
                return NotFound("No rainfall data found");
            }

            return Ok(allRainfallData);
        }
    }
}
