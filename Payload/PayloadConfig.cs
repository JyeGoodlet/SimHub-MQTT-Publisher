using System.Collections.Generic;

namespace SimHub.MQTTPublisher.Payload
{
    public class PayloadConfig
    {
        /// <summary>
        /// List of fields to include in the MQTT payload. Each entry is either a built-in
        /// field name ("time", "userId") or any property name on SimHub's StatusDataBase
        /// (e.g. "SpeedKmh", "Flag_Name"). Add any SimHub property here without code changes.
        /// </summary>
        public List<string> Fields { get; set; } = new List<string>();

        public static PayloadConfig CreateDefault()
        {
            return new PayloadConfig
            {
                Fields = new List<string>
                {
                    "time",
                    "userId",
                    "SpeedKmh",
                    "Rpms",
                    "Clutch",
                    "Throttle",
                    "Brake",
                    "Gear",
                    "CarCoordinates",
                    "CurrentLapTime",
                    "CarModel",
                    "CarClass",
                    "EngineIgnitionOn",
                    "EngineStarted",
                    "Flag_Name",
                    "Flag_Black",
                    "Flag_Blue",
                    "Flag_Checkered",
                    "Flag_Yellow",
                    "Flag_Green",
                    "Flag_White",
                    "Flag_Orange"
                }
            };
        }
    }
}

