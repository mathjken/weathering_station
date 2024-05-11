using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sensors;

namespace EnvironmentalMonitorAPI.Tests.Sensors.Tests
{
   public class CO2EmissionsSensorTests
    {
        [Fact]
        public void GetCO2Emissions_ShouldReturnRandomValueBetweenOneAndOneHundred()
        {
            var co2EmissionsSensor = new CO2EmissionsSensor();
            var co2Value = co2EmissionsSensor.GetCO2Emissions();

            Assert.True(co2Value >= 1 && co2Value <= 100, "CO2 emission value should be between 1 and 100");
        }
    }

}
