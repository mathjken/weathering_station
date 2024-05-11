using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sensors;


namespace EnvironmentalMonitorAPI.Tests.Sensors.Tests
{
    public class TemperatureSensorTests
    {
        [Fact]
        public void GetTemperature_ShouldReturnRandomValueBetweenNegativeTwentyAndThirtyEight()
        {
            var temperatureSensor = new TemperatureSensor();
            var temperatureValue = temperatureSensor.GetTemperature();

            Assert.True(temperatureValue >= -20 && temperatureValue <= 38, "Temperature value should be between -20 and 38");
        }
    }

}
