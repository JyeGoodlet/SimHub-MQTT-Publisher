using GameReaderCommon;
using System;

namespace SimHub.MQTTPublisher.Payload
{
    public class PayloadRoot
    {
        private readonly PayloadSectionConfig _config;

        public PayloadRoot(GameData data, SimHubMQTTPublisherPluginUserSettings userSettings, PayloadConfig config)
        {
            _config = config.PayloadRoot;
            time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            carState = new Car(data, config);
            userId = userSettings.UserId.ToString();
            flagData = new FlagInformation(data, config);
        }

        public long time { get; set; }
        public string userId { get; set; }
        public Car carState { get; set; }
        public FlagInformation flagData { get; set; }

        public bool ShouldSerializetime() => _config.EnabledFields.Contains("time");
        public bool ShouldSerializeuserId() => _config.EnabledFields.Contains("userId");
        public bool ShouldSerializecarState() => _config.EnabledFields.Contains("carState");
        public bool ShouldSerializeflagData() => _config.EnabledFields.Contains("flagData");
    }
}