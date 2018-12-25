using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace SimpleIPBan
{
    class CommandIPUnban : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "IPUnban";
        public string Help => "Unbans an IP.";
        public string Syntax => "<IP>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>() { "ipban.ipunban" };


        public void Execute(IRocketPlayer rCaller, params string[] Command)
        {
            if (Command.Length > 1)
            {
                UnturnedChat.Say(rCaller, $"/IPUnban {Syntax}", Color.red);
                return;
            }

            if (SimpleIPBan.Instance.BlacklistedIPs.Contains(Command[0]))
            {
                SimpleIPBan.Instance.RemoveIP(rCaller, Command[0]);
            }


        }
    }
}
