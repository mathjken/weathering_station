using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Sensors;
using SensorsAPI.Models;

namespace SensorsAPI.Controllers
{
    [Route("api/co2-emissions-sensor")]
    [ApiController]
    public class CO2EmissionsSensorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CO2EmissionsSensorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<double>> GetCO2Emissions(int sensorId)
        {
            try
            {
                var Sensor = new CO2EmissionsSensor();
                double co2EmissionsValue = Sensor.GetCO2Emissions();

                string warning= "Normal";

                TimeZoneInfo ukTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime ukTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukTimeZone);

                var co2EmissionsData = new SensorsData
                {
                    id = 0,
                    SensorId = sensorId,
                    Parameter = "CO2 Emissions",
                    Unit = "t CO2e",
                    ParameterValue = co2EmissionsValue,
                    TimeStamp = ukTime,
                    Warning = warning,
                    DataCollectionInterval = "120 minutes",
                    DataRangeMin = 1,
                    DataRangeMax = 100,
                    NormalThresholdMin = null,
                    NormalThresholdMax = null,
                };

                var jsonData = JsonSerializer.Serialize(co2EmissionsData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7263/");

                    var response = await httpClient.PostAsync("api/co2-emissions-monitoring-station", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Air Pollution data with {sensorId} sensor id added successfully -> ",response.Content);
                        return Ok(co2EmissionsData);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Sending CO2 emissions data to Monitoring Station API FAILED.");
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
