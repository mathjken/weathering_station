using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;
using SensorsAPI.Models;

namespace SensorsAPI.Controllers
{
    [Route("api/temperature-sensor")]
    [ApiController]
    public class TemperatureSensorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TemperatureSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<double>> GetTemperature(int sensorId)
        {
            try
            {
                var Sensor = new TemperatureSensor();
                double temperatureValue = Sensor.GetTemperature();

                string warning= "Normal";

                if(temperatureValue > 30)
                {
                    warning = "High";
                    Console.WriteLine($"Warning: Temperature levels have crossed the normal threshold with {temperatureValue} C.");
                }
                else if(temperatureValue < -9)
                {
                    warning = "Low";
                    Console.WriteLine($"Warning: Temperature levels is below the normal threshold with {temperatureValue} C.");
                }

                TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

                var temperatureData = new SensorsData
                {
                    id = 0,
                    SensorId = sensorId,
                    Parameter = "temperature",
                    Unit = "Celcius",
                    ParameterValue = temperatureValue,
                    TimeStamp = ukTime,
                    Warning = warning,
                    DataCollectionInterval = "30 minutes",
                    DataRangeMin = -20,
                    DataRangeMax = 39,
                    NormalThresholdMin = -9,
                    NormalThresholdMax = 30,
                };

                var jsonData = JsonSerializer.Serialize(temperatureData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send POST request to Monitoring Station API
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7263/"); // Replace with Monitoring Station API base URL

                    var response = await httpClient.PostAsync("api/temperature-monitoring-station", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Temperature data with {sensorId} sensor id added successfully -> ",response.Content);
                        return Ok(temperatureData);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Sending temperature data to Monitoring Station API FAILED.");
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
