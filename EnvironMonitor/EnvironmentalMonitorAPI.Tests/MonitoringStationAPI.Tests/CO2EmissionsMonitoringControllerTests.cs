using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using AirPollutionMonitoringController.Controllers;
using MonitoringStationAPI.Database;
using MonitoringStationAPI.Models;
using Xunit;


namespace EnvironmentalMonitorAPI.Tests.MonitoringStationAPI.Tests
{
    public class CO2EmissionsMonitoringControllerTests
    {
        [Fact]
        public void PostCO2EmissionsData_WithValidData_ShouldReturnOk()
        {
            // Arrange (set up mocks and test data)
            var mockDbContext = new Mock<EnvironmentalDataContext>();
            mockDbContext.Setup(x => x.SaveChanges()).Verifiable(); // Verify SaveChanges is called

            var controller = new CO2EmissionsMonitoringController(mockDbContext.Object);
            var co2EmissionsData = new Sensor { Id = 1, DateTime = DateTime.UtcNow, Value = 38.5f };

            // Act (call the controller method)
            var result = controller.PostCO2EmissionsData(co2EmissionsData);

            // Assert (verify the outcome)
            Assert.IsType<OkObjectResult>(result); // Check for Ok response
            var objectResult = (OkObjectResult)result;
            Assert.Equal(co2EmissionsData, objectResult.Value); // Check returned data matches input

            // Verify mock expectations
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
