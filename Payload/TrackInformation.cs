using GameReaderCommon;

namespace SimHub.MQTTPublisher.Payload
{
    public class TrackInformation
    {
        private readonly PayloadSectionConfig _config;

        public TrackInformation(GameData data, PayloadConfig config)
        {
            _config = config.TrackInformation;
            this.TrackId = data.NewData.TrackId;
            this.TrackConfig = data.NewData.TrackConfig;
            this.TrackCode = data.NewData.TrackCode;
            this.TrackLength = data.NewData.TrackLength;
        }

        public string TrackId { get; set; }

        public string TrackConfig { get; set; }

        public string TrackCode { get; set; }

        public double TrackLength { get; set; }

        public bool ShouldSerializeTrackId() => _config.EnabledFields.Contains("TrackId");
        public bool ShouldSerializeTrackConfig() => _config.EnabledFields.Contains("TrackConfig");
        public bool ShouldSerializeTrackCode() => _config.EnabledFields.Contains("TrackCode");
        public bool ShouldSerializeTrackLength() => _config.EnabledFields.Contains("TrackLength");
    }
}