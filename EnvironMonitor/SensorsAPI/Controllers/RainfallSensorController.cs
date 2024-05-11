using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;
using SensorsAPI.Models;

namespace SensorsAPI.Controllers
{
    [Route("api/rainfall-sensor")]
    [ApiController]
    public class RainfallSensorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RainfallSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<double>> GetRainfall(int sensorId)
        {
            try
            {
                var Sensor = new RainfallSensor();
                double rainfallValue = Sensor.GetRainfall();

                string warning= "Normal";

                if(rainfallValue > 32)
                {
                    warning = "High";
                    Console.WriteLine($"Warning: Temperature levels have crossed the normal threshold with {rainfallValue} C.");
                }
                else if(rainfallValue < 0)
                {
                    warning = "Low";
                    Console.WriteLine($"Warning: Temperature levels is below the normal threshold with {rainfallValue} C.");
                }
                    Console.WriteLine($"Warning: Temperature levels have crossed the normal threshold with 38 C.");

                TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

                var rainfallData = new SensorsData
                {
                    id = 0,
                    SensorId = sensorId,
                    Parameter = "rainfall",
                    Unit = "Millimetres",
                    ParameterValue = rainfallValue,
                    TimeStamp = ukTime,
                    Warning = warning,
                    DataCollectionInterval = "30 minutes",
                    DataRangeMin = 0,
                    DataRangeMax = 40,
                    NormalThresholdMin = 0,
                    NormalThresholdMax = 32,
                };

                var jsonData = JsonSerializer.Serialize(rainfallData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send POST request to Monitoring Station API
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7263/"); // Replace with Monitoring Station API base URL

                    var response = await httpClient.PostAsync("api/rainfall-monitoring-station", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Rainfall data with {sensorId} sensor id added successfully -> ",response.Content);
                        return Ok(rainfallData);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Sending rainfall data to Monitoring Station API FAILED.");
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
