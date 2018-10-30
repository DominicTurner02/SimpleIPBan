using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using Logger = Rocket.Core.Logging.Logger;

namespace SimpleIPBan
{
    public class SimpleIPBan : RocketPlugin<ConfigurationSimpleIPBan>
    {
        public static SimpleIPBan Instance { get; private set; }

        protected override void Load()
        {
            base.Load();
            Instance = this;

            Logger.LogWarning("\n Loading SimpleIPBan, made by Mr.Kwabs...");

            Logger.LogWarning($"\n Kick Reason: {Configuration.Instance.KickReason}");
            Logger.LogWarning($"\n Found {Instance.Configuration.Instance.BannedIPs.Count} blacklisted IPs: ");
            foreach (string IP in Instance.Configuration.Instance.BannedIPs)
            {
                Logger.LogWarning($" {IP}");
            }
            Logger.LogWarning("\n Successfully loaded SimpleIPBan, made by Mr.Kwabs!");

            U.Events.OnPlayerConnected += OnPlayerConnected;

        }


        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            Instance = null;
            base.Unload();
        }


        private void OnPlayerConnected(UnturnedPlayer player)
        {
            
            if (Instance.Configuration.Instance.BannedIPs.Contains(player.IP))
            {
                player.Kick(Instance.Configuration.Instance.KickReason);
                Logger.LogWarning($"{player.DisplayName} was kicked as their IP is blacklisted! [{player.IP}]");
            }

        }

    }
}
