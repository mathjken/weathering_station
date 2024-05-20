using System;
using System.Collections.Generic;

namespace Sensors
{
    public class SensorDataGenerator
    {
        public List<Sensor> GenerateRandomSensorData()
        {
            var random = new Random();
            var sensors = new List<Sensor>
            {
                new Sensor
                {
                    Name = "AirPollutionSensor",
                    ParameterValue = random.NextDouble() * 10,
                    Timestamp = DateTime.UtcNow
                },
                new Sensor
                {
                    Name = "CO2EmissionSensor",
                    ParameterValue = random.Next(300, 500),
                    Timestamp = DateTime.UtcNow
                },
                new Sensor
                {
                    Name = "RainfallSensor",
                    ParameterValue = random.NextDouble() * 100,
                    Timestamp = DateTime.UtcNow
                },
                new Sensor
                {
                    Name = "TemperatureSensor",
                    ParameterValue = random.NextDouble() * 50 - 10,
                    Timestamp = DateTime.UtcNow
                },
                new Sensor
                {
                    Name = "HumiditySensor",
                    ParameterValue = random.NextDouble() * 100,
                    Timestamp = DateTime.UtcNow
                }
            };

            return sensors;
        }
    }
}
