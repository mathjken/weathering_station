using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sensors;

namespace EnvironmentalMonitorAPI.Tests.Sensors.Tests
{
    public class HumiditySensorTests
    {
        [Fact]
        public void GetHumidity_ShouldReturnRandomValueBetweenZeroAndOneHundred()
        {
            var humiditySensor = new HumiditySensor();
            var humidityValue = humiditySensor.GetHumidity();

            Assert.True(humidityValue >= 0 && humidityValue <= 100, "Humidity value should be between 0 and 100");
        }
    }

}
