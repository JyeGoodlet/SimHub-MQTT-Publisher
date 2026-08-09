using GameReaderCommon;
using System.Collections.Generic;
using System.Linq;

namespace SimHub.MQTTPublisher.Payload
{
    public class Car
    {
        private readonly PayloadSectionConfig _config;

        public Car(GameData data, PayloadConfig config)
        {
            _config = config.Car;
            SpeedKmh = data.NewData.SpeedKmh;
            Rpms = data.NewData.Rpms;
            Clutch = data.NewData.Clutch;
            Throttle = data.NewData.Throttle;
            Brake = data.NewData.Brake;
            Gear = data.NewData.Gear;
            CarCoordinates = data.NewData.CarCoordinates.ToList();
            CurrentLapTime = data.NewData.CurrentLapTime.TotalMilliseconds;
            CarModel = data.NewData.CarModel;
            CarClass = data.NewData.CarClass;
            EngineIgnitionOn = data.NewData.EngineIgnitionOn == 1;
            EngineStarted = data.NewData.EngineStarted == 1;
        }

        public double SpeedKmh { get; set; }
        public double Rpms { get; set; }
        public double Brake { get; set; }
        public double Throttle { get; set; }
        public double Clutch { get; set; }
        public string Gear { get; set; }
        public List<double> CarCoordinates { get; set; }
        public double CurrentLapTime { get; set; }
        public string CarModel { get; set; }
        public string CarClass { get; set; }
        public bool EngineIgnitionOn { get; set; }
        public bool EngineStarted { get; set; }

        public bool ShouldSerializeSpeedKmh() => _config.EnabledFields.Contains("SpeedKmh");
        public bool ShouldSerializeRpms() => _config.EnabledFields.Contains("Rpms");
        public bool ShouldSerializeBrake() => _config.EnabledFields.Contains("Brake");
        public bool ShouldSerializeThrottle() => _config.EnabledFields.Contains("Throttle");
        public bool ShouldSerializeClutch() => _config.EnabledFields.Contains("Clutch");
        public bool ShouldSerializeGear() => _config.EnabledFields.Contains("Gear");
        public bool ShouldSerializeCarCoordinates() => _config.EnabledFields.Contains("CarCoordinates");
        public bool ShouldSerializeCurrentLapTime() => _config.EnabledFields.Contains("CurrentLapTime");
        public bool ShouldSerializeCarModel() => _config.EnabledFields.Contains("CarModel");
        public bool ShouldSerializeCarClass() => _config.EnabledFields.Contains("CarClass");
        public bool ShouldSerializeEngineIgnitionOn() => _config.EnabledFields.Contains("EngineIgnitionOn");
        public bool ShouldSerializeEngineStarted() => _config.EnabledFields.Contains("EngineStarted");
    }
}