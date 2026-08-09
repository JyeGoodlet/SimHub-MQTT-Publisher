using GameReaderCommon;

namespace SimHub.MQTTPublisher.Payload
{
    public class VehicleInformation
    {
        private readonly PayloadSectionConfig _config;

        public VehicleInformation(GameData data, PayloadConfig config)
        {
            _config = config.VehicleInformation;
            this.CarModel = data.NewData.CarModel;
            this.CarClass = data.NewData.CarClass;
            this.CarId = data.NewData.CarId;
            this.MaxRpm = data.NewData.MaxRpm;
        }

        public string CarModel { get; set; }

        public string CarClass { get; set; }

        public string CarId { get; set; }

        public double MaxRpm { get; set; }

        public bool ShouldSerializeCarModel() => _config.EnabledFields.Contains("CarModel");
        public bool ShouldSerializeCarClass() => _config.EnabledFields.Contains("CarClass");
        public bool ShouldSerializeCarId() => _config.EnabledFields.Contains("CarId");
        public bool ShouldSerializeMaxRpm() => _config.EnabledFields.Contains("MaxRpm");
    }
}