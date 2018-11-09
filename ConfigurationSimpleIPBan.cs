using Rocket.API;

namespace SimpleIPBan
{
    public class ConfigurationSimpleIPBan : IRocketPluginConfiguration
    {
        public bool KickOnIPBan;
        public string KickReason;

        public void LoadDefaults()
        {
            KickOnIPBan = true;
            KickReason = "You are IP banned!";
        }

    }
}
