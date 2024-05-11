using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sensors;

namespace EnvironmentalMonitorAPI.Tests.Sensors.Tests
{
    public class AirPollutionSensorTests
    {
        [Fact]
        public void GetAirPollution_ShouldReturnRandomValueBetweenOneAndTen()
        {
            var airPollutionSensor = new AirPollutionSensor();
            var airPollutionValue = airPollutionSensor.GetAirPollution();

            Assert.True(airPollutionValue >= 1 && airPollutionValue <= 10, "Air pollution value should be between 1 and 10");
        }
    }
}
