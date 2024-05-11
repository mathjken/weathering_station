using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Controllers;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using Moq;
using System.Linq;
using Xunit;

namespace RainfallMonitoringController.Tests
{
    public class RainfallMonitoringControllerTests
    {
        [Fact]
        public void PostRainfallData_WithValidData_ShouldReturnOk()
        {
            // Arrange
            var mockDbContext = new Mock<EnvironmentalDataContext>();
            var controller = new RainfallMonitoringController(mockDbContext.Object);
            var rainfallData = new Sensor { Id = 1, ParameterValue = 25.3f };

            // Act
            var result = controller.PostRainfallData(rainfallData);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;
            Assert.Equal(rainfallData, objectResult.Value);

            // Verify
            mockDbContext.Verify(x => x.Sensor.Add(It.IsAny<Sensor>()), Times.Once);
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
