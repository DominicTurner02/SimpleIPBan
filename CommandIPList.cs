using Rocket.API;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SimpleIPBan
{
    class CommandIPList : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "IPList";
        public string Help => "Sends blacklisted IPs to console.";
        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>() { "ipban.iplist" };


        public void Execute(IRocketPlayer caller, params string[] command)
        {
            if (SimpleIPBan.Instance.BlacklistedIPs.Count != 0)
            {
                UnturnedChat.Say(caller, "Listing Blacklisted IPs: ", Color.yellow);
                foreach (string IP in SimpleIPBan.Instance.BlacklistedIPs)
                {
                    Logger.LogWarning($"{IP}");
                }
            } else
            {
                UnturnedChat.Say(caller, "There are no blacklisted IPs.", Color.red);
            }

        }
    }
}
