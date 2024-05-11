using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;
using SensorsAPI.Models;

namespace SensorsAPI.Controllers
{
    [Route("api/humidity-sensor")]
    [ApiController]
    public class HumiditySensorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HumiditySensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<double>> GetHumidity(int sensorId)
        {
            try
            {
                var Sensor = new HumiditySensor();
                double humidityValue = Sensor.GetHumidity();

                string warning= "Normal";

                TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

                var rainfallData = new SensorsData
                {
                    id = 0,
                    SensorId = sensorId,
                    Parameter = "humidity",
                    Unit = "Percentage",
                    ParameterValue = humidityValue,
                    TimeStamp = ukTime,
                    Warning = warning,
                    DataCollectionInterval = "60 minutes",
                    DataRangeMin = 0,
                    DataRangeMax = 100,
                    NormalThresholdMin = null,
                    NormalThresholdMax = null,
                };

                var jsonData = JsonSerializer.Serialize(rainfallData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send POST request to Monitoring Station API
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7263/"); // Replace with Monitoring Station API base URL

                    var response = await httpClient.PostAsync("api/humidity-monitoring-station", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Humidity data with {sensorId} sensor id added successfully -> ",response.Content);
                        return Ok(rainfallData);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Sending humidity data to Monitoring Station API FAILED.");
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
