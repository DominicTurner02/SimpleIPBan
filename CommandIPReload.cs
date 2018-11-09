using Rocket.API;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleIPBan
{
    class CommandIPReload : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "IPReload";
        public string Help => "Reloads the IP blacklist.";
        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>() { "ipban.ipreload" };


        public void Execute(IRocketPlayer caller, params string[] command)
        {
            SimpleIPBan.Instance.ReloadList();
            UnturnedChat.Say(caller, "Successfully reloaded IP Blacklist", Color.yellow);
        }
    }
}
