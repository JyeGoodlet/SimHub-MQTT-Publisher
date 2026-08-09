using GameReaderCommon;

namespace SimHub.MQTTPublisher.Payload
{
    public class FlagInformation
    {
        private readonly PayloadSectionConfig _config;

        public FlagInformation(GameData data, PayloadConfig config)
        {
            _config = config.FlagInformation;
            this.Flag_Name = data.NewData.Flag_Name;
			this.Flag_Black = data.NewData.Flag_Black;
			this.Flag_Blue = data.NewData.Flag_Blue;
			this.Flag_Checkered = data.NewData.Flag_Checkered;
			this.Flag_Yellow = data.NewData.Flag_Yellow;
			this.Flag_Green = data.NewData.Flag_Green;
			this.Flag_White = data.NewData.Flag_White;
			this.Flag_Orange = data.NewData.Flag_Orange;
		}

		public string Flag_Name { get; set; }
		public int Flag_Black { get; set; }
        public int Flag_Blue { get; set; }
        public int Flag_Checkered { get; set; }
        public int Flag_Yellow { get; set; }
        public int Flag_Green { get; set; }
        public int Flag_White { get; set; }
		public int Flag_Orange { get; set; }

		public bool ShouldSerializeFlag_Name() => _config.EnabledFields.Contains("Flag_Name");
		public bool ShouldSerializeFlag_Black() => _config.EnabledFields.Contains("Flag_Black");
		public bool ShouldSerializeFlag_Blue() => _config.EnabledFields.Contains("Flag_Blue");
		public bool ShouldSerializeFlag_Checkered() => _config.EnabledFields.Contains("Flag_Checkered");
		public bool ShouldSerializeFlag_Yellow() => _config.EnabledFields.Contains("Flag_Yellow");
		public bool ShouldSerializeFlag_Green() => _config.EnabledFields.Contains("Flag_Green");
		public bool ShouldSerializeFlag_White() => _config.EnabledFields.Contains("Flag_White");
		public bool ShouldSerializeFlag_Orange() => _config.EnabledFields.Contains("Flag_Orange");
	}
}