using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleIPBan
{
    public class ConfigurationSimpleIPBan : IRocketPluginConfiguration
    {
        public string KickReason;
        [XmlArrayItem(ElementName = "BannedIP")]
        public List<string> BannedIPs;

        public void LoadDefaults()
        {
            KickReason = "You are IP banned!";
            BannedIPs = new List<string>()
            {
                "000.00.00.000"
            };

        }

    }
}
