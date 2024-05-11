// namespace MonitoringStationAPI.Models
// {
//     public class Sensor
//     {
//         public int Id { get; set; }
//         public string? ParameterType { get; set; }
//         public float ParameterValue { get; set; }
//         public string? DataCollectionInterval { get; set; }
//         public DateTime Timestamp { get; set; }
//         public float DataRangeMinimum { get; set; }
//         public float DataRangeMax { get; set; }
//         public string? Unit { get; set; }
//         public float? NormalThresholdMin { get; set; }
//         public float? NormalThresholdMax { get; set; }
//     }
// }
namespace MonitoringStationAPI.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public string? Parameter { get; set; }
        public string? Unit { get; set; }
        public double ParameterValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Warning { get; set; }
        public string? DataCollectionInterval { get; set; }
        public float DataRangeMin { get; set; }
        public float DataRangeMax { get; set; }
        public float? NormalThresholdMin { get; set; }
        public float? NormalThresholdMax { get; set; }
    }
}
