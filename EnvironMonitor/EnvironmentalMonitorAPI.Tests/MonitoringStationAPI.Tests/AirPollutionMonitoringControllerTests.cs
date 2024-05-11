using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using AirPollutionMonitoringController.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnvironmentalMonitorAPI.Tests.MonitoringStationAPI.Tests
{
    public class AirPollutionMonitoringControllerTests
    {
        [Fact]
        public void PostAirPollutionData_WithValidData_ShouldReturnOk()
        {
            // Arrange (set up mocks and test data)
            var mockDbContext = new Mock<EnvironmentalDataContext>();
            mockDbContext.Setup(x => x.SaveChanges()).Verifiable(); // Verify SaveChanges is called

            var controller = new AirPollutionMonitoringController(mockDbContext.Object);
            var airPollutionData = new Sensor { Id = 1, DateTime = DateTime.UtcNow, Value = 5.2f };

            // Act (call the controller method)
            var result = controller.PostAirPollutionData(airPollutionData);

            // Assert (verify the outcome)
            Assert.IsType<OkObjectResult>(result); // Check for Ok response
            var objectResult = (OkObjectResult)result;
            Assert.Equal(airPollutionData, objectResult.Value); // Check returned data matches input

            // Verify mock expectations
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}



