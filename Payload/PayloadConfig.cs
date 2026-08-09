using System.Collections.Generic;

namespace SimHub.MQTTPublisher.Payload
{
    public class PayloadSectionConfig
    {
        public HashSet<string> EnabledFields { get; set; } = new HashSet<string>();
    }

    public class PayloadConfig
    {
        public PayloadSectionConfig PayloadRoot { get; set; } = new PayloadSectionConfig();
        public PayloadSectionConfig Car { get; set; } = new PayloadSectionConfig();
        public PayloadSectionConfig FlagInformation { get; set; } = new PayloadSectionConfig();
        public PayloadSectionConfig TrackInformation { get; set; } = new PayloadSectionConfig();
        public PayloadSectionConfig VehicleInformation { get; set; } = new PayloadSectionConfig();

        public static PayloadConfig CreateDefault()
        {
            return new PayloadConfig
            {
                PayloadRoot = new PayloadSectionConfig
                {
                    EnabledFields = new HashSet<string> { "time", "userId", "carState", "flagData" }
                },
                Car = new PayloadSectionConfig
                {
                    EnabledFields = new HashSet<string>
                    {
                        "SpeedKmh", "Rpms", "Clutch", "Throttle", "Brake", "Gear",
                        "CarCoordinates", "CurrentLapTime", "CarModel", "CarClass",
                        "EngineIgnitionOn", "EngineStarted"
                    }
                },
                FlagInformation = new PayloadSectionConfig
                {
                    EnabledFields = new HashSet<string>
                    {
                        "Flag_Name", "Flag_Black", "Flag_Blue", "Flag_Checkered",
                        "Flag_Yellow", "Flag_Green", "Flag_White", "Flag_Orange"
                    }
                },
                TrackInformation = new PayloadSectionConfig
                {
                    EnabledFields = new HashSet<string> { "TrackId", "TrackConfig", "TrackCode", "TrackLength" }
                },
                VehicleInformation = new PayloadSectionConfig
                {
                    EnabledFields = new HashSet<string> { "CarModel", "CarClass", "CarId", "MaxRpm" }
                }
            };
        }
    }
}
