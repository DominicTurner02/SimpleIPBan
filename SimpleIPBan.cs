using Newtonsoft.Json.Linq;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.IO;
using Logger = Rocket.Core.Logging.Logger;

namespace SimpleIPBan
{
    public class SimpleIPBan : RocketPlugin<ConfigurationSimpleIPBan>
    {
        public static SimpleIPBan Instance { get; private set; }
        public List<string> BlacklistedIPs = new List<string>();

        protected override void Load()
        {
            base.Load();
            Instance = this;

            Logger.LogWarning("\n Loading SimpleIPBan, made by Mr.Kwabs...");
            if (!File.Exists("Plugins/SimpleIPBan/IPBlacklist.json"))
            {
                Logger.LogError("\n IPBlacklist.json does not exist, creating it now...");
                FirstLoad();
                Logger.LogWarning(" Successfully created IPBlacklist.json.");
            }

            SendList();
            Logger.LogWarning($"\n Found IPBlacklist.json, read {BlacklistedIPs.Count} IPs.");
            Logger.LogWarning("\n IP List: \n");
            foreach (string IP in BlacklistedIPs)
            {
                Logger.LogWarning($" {IP}");
            }
        
            
            

            if (Instance.Configuration.Instance.KickOnIPBan)
            {
                Logger.LogWarning("\n Kick on IP Ban: Enabled");
            } else
            {
                Logger.LogError("\n Kick on IP Ban: Disabled");
            }
            
            Logger.LogWarning($"\n Kick Reason: {Configuration.Instance.KickReason}");
          
            Logger.LogWarning("\n Successfully loaded SimpleIPBan, made by Mr.Kwabs!");


            U.Events.OnPlayerConnected += OnPlayerConnected;

        }

        private void FirstLoad()
        {
            JArray IPBlacklist = new JArray(
                new JArray("000.00.00.000", "000.00.00.001"));
            File.WriteAllText("Plugins/SimpleIPBan/IPBlacklist.json", IPBlacklist.ToString());
        }

        private void SendList()
        {
            JArray IPArray = JArray.Parse(File.ReadAllText("Plugins/SimpleIPBan/IPBlacklist.json"));
            foreach (string IP in IPArray)
            {
                BlacklistedIPs.Add(IP);
            }
        }

        public void ReloadList()
        {
            BlacklistedIPs.Clear();
            JArray IPArray = JArray.Parse(File.ReadAllText("Plugins/SimpleIPBan/IPBlacklist.json"));
            foreach (string IP in IPArray)
            {
                BlacklistedIPs.Add(IP);
            }
        }


        protected override void Unload()
        {
            BlacklistedIPs.Clear();
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            Instance = null;
            base.Unload();
        }


        private void OnPlayerConnected(UnturnedPlayer player)
        {
            if (BlacklistedIPs.Contains(player.IP))
            {
                player.Kick(Instance.Configuration.Instance.KickReason);
                Logger.LogWarning($"{player.DisplayName} was kicked as their IP ({player.IP}) is blacklisted!");
            }
        }

        public void AddIP(IRocketPlayer Caller, string IP)
        {
            try
            {
                JArray IPArray = JArray.Parse(File.ReadAllText("Plugins/SimpleIPBan/IPBlacklist.json"));
                IPArray.Add(IP);
                File.WriteAllText("Plugins/SimpleIPBan/IPBlacklist.json", IPArray.ToString());
                BlacklistedIPs.Add(IP);
                UnturnedChat.Say(Caller, $"Successfully added {IP} to the blacklist.");
            }
            catch (Exception ex)
            {
                UnturnedChat.Say(Caller, "There was an issue running this command. Please check your Rocket.log and contact the Author for support. \n");
                Logger.LogError($"{ex}");
            }
        }


        public void AddPlayerIP(IRocketPlayer Caller, UnturnedPlayer Victim, string IP)
        {

            try
            {
                JArray IPArray = JArray.Parse(File.ReadAllText("Plugins/SimpleIPBan/IPBlacklist.json"));
                IPArray.Add(Victim.IP);
                File.WriteAllText("Plugins/SimpleIPBan/IPBlacklist.json", IPArray.ToString());
                BlacklistedIPs.Add(Victim.IP);
                UnturnedChat.Say(Caller, $"Successfully added {Victim.DisplayName}'s IP ({Victim.IP}) to the blacklist.");

                if (Instance.Configuration.Instance.KickOnIPBan)
                {
                    Victim.Kick(Instance.Configuration.Instance.KickReason);
                }

            } catch (Exception ex)
            {
                UnturnedChat.Say(Caller, "There was an issue running this command. Please check your Rocket.log and contact the Author for support. \n");
                Logger.LogError($"{ex}");
            }
        }
    }
}
