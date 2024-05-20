using SensorsAPI.Controllers;
using MonitoringStationAPI.Database;
using Sensors;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EnvironmentalDataContext>();
    var sensorDataGenerator = new SensorDataGenerator();
    var sensorData = sensorDataGenerator.GenerateRandomSensorData().Select(sensor => new MonitoringStationAPI.Models.Sensor
    {
        Name = sensor.Name,
        Type = sensor.Type,
        ParameterValue = sensor.ParameterValue,
        Timestamp = sensor.Timestamp
    }).ToList();

    dbContext.Sensor.AddRange(sensorData);
    dbContext.SaveChanges();
}

app.Run();
