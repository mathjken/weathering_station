using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Controllers;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using Moq;
using System.Linq;
using Xunit;

namespace HumidityMonitoringController.Tests
{
    public class HumidityMonitoringControllerTests
    {
        [Fact]
        public void PostHumidityData_WithValidData_ShouldReturnOk()
        {
            // Arrange
            var mockDbContext = new Mock<EnvironmentalDataContext>();
            var controller = new HumidityMonitoringController(mockDbContext.Object);
            var humidityData = new Sensor { Id = 1, ParameterValue = 50.5f };

            // Act
            var result = controller.PostHumidityData(humidityData);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;
            Assert.Equal(humidityData, objectResult.Value);

            // Verify
            mockDbContext.Verify(x => x.Sensor.Add(It.IsAny<Sensor>()), Times.Once);
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }

    }
}
