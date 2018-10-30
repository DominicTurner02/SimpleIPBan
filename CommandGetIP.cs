using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SimpleIPBan
{
    class CommandGetIP : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "GetIP";
        public string Help => "Gets the IP of a Player.";
        public string Syntax => "<Player || SteamID>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>() { "ipban.getip" };


        public void Execute(IRocketPlayer caller, params string[] command)
        {
            try
            {
                if (command.Length != 0 || command.Length <= 1)
                {
                    bool Skip = new bool();
                    UnturnedPlayer Victim = null;
                    try
                    {
                        Victim = UnturnedPlayer.FromName(command[0]);
                    }
                    catch
                    {
                        UnturnedChat.Say(caller, "User not found!", Color.red);
                        Skip = true;
                    }

                    if (!Skip)
                    {
                        UnturnedChat.Say(caller, $"{Victim.DisplayName}'s IP is: {Victim.IP}", Color.yellow);
                        Logger.LogWarning($"{Victim.DisplayName}'s IP is: {Victim.IP}");
                       
                    }
                } else
                {
                    UnturnedChat.Say(caller, "Incorrect Usage: /GetIP [Name || SteamID]", Color.red);
                }
            } catch
            {
                UnturnedChat.Say(caller, "User not found!", Color.red);
            }
        }
    }
}
