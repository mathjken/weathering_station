using Microsoft.AspNetCore.Mvc;
//using MonitoringStationAPI.Controllers;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using Moq;
using System.Linq;
using Xunit;

namespace TemperatureMonitoringController.Tests
{
    public class TemperatureMonitoringControllerTests
    {
        [Fact]
        public void PostTemperatureData_WithValidData_ShouldReturnOk()
        {
            // Arrange
            var mockDbContext = new Mock<EnvironmentalDataContext>();
            var controller = new TemperatureMonitoringController(mockDbContext.Object);
            var temperatureData = new Sensor { Id = 1, ParameterValue = 30.5f };

            // Act
            var result = controller.PostTemperatureData(temperatureData);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;
            Assert.Equal(temperatureData, objectResult.Value);

            // Verify
            mockDbContext.Verify(x => x.Sensor.Add(It.IsAny<Sensor>()), Times.Once);
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
