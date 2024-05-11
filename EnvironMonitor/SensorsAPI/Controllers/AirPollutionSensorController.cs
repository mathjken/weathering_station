using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;
using SensorsAPI.Models;

namespace SensorsAPI.Controllers
{
    [Route("api/air-pollution-sensor")]
    [ApiController]
    public class AirPollutionSensorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AirPollutionSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<double>> GetAirPollution(int sensorId)
        {
            try
            {
                var Sensor = new AirPollutionSensor();
                double airPollutionValue = Sensor.GetAirPollution();

                string warning= "Normal";

                if (airPollutionValue > 9)
                {
                    warning = "High";
                    Console.WriteLine($"Warning: Air pollution levels have crossed the normal threshold with {airPollutionValue}.");
                }
                else if (airPollutionValue < 1)
                {
                    warning = "Low";
                    Console.WriteLine($"Warning: Air pollution levels is below the normal threshold with {airPollutionValue}.");
                }

                TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

                var rainfallData = new SensorsData
                {
                    id = 0,
                    SensorId = sensorId,
                    Parameter = "air pollution",
                    Unit = "AQI",
                    ParameterValue = airPollutionValue,
                    TimeStamp = ukTime,
                    Warning = warning,
                    DataCollectionInterval = "60 minutes",
                    DataRangeMin = 1,
                    DataRangeMax = 10,
                    NormalThresholdMin = 1,
                    NormalThresholdMax = 9,
                };

                var jsonData = JsonSerializer.Serialize(rainfallData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7263/");

                    var response = await httpClient.PostAsync("api/air-pollution-monitoring-station", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Air Pollution data with {sensorId} sensor id added successfully -> ",response.Content);
                        return Ok(rainfallData);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Sending Air Pollution data to Monitoring Station API FAILED.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }

        }
    }
}
