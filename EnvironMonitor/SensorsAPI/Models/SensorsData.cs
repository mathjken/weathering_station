namespace SensorsAPI.Models
{
    public class SensorsData
    {
        public int id { get; set; }
        public int SensorId { get; set; }
        public string? Parameter { get; set; }
        public string? Unit { get; set; }
        public double ParameterValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Warning { get; set; }
        public string? DataCollectionInterval { get; set; }
        public double DataRangeMin { get; set; }
        public double DataRangeMax { get; set; }
        public double? NormalThresholdMin { get; set; }
        public double? NormalThresholdMax { get; set; }
    }
}
