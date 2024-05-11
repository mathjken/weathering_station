using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sensors;


namespace EnvironmentalMonitorAPI.Tests.Sensors.Tests
{
    public class RainfallSensorTests
    {
        [Fact]
        public void GetRainfall_ShouldReturnRandomValueBetweenZeroAndThirtyFive()
        {
            var rainfallSensor = new RainfallSensor();
            var rainfallValue = rainfallSensor.GetRainfall();

            Assert.True(rainfallValue >= 0 && rainfallValue <= 35, "Rainfall value should be between 0 and 35");
        }
    }

}
