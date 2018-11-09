using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace SimpleIPBan
{
    class CommandIPBan : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "IPBan";
        public string Help => "Bans a certain players IP.";
        public string Syntax => "<Player || SteamID || IP>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>() { "ipban.ipban" };


        public void Execute(IRocketPlayer caller, params string[] command)
        {
            try
            {
                if (command.Length != 0 || command.Length <= 1)
                {
                    bool IsIP = new bool();
                    bool IsPlayer = false;
                    string IP = "";
                    UnturnedPlayer Victim = null;
                    try
                    {
                        Victim = UnturnedPlayer.FromName(command[0]);
                        try
                        {
                            IP = Victim.IP;
                            IsPlayer = true;
                        } catch
                        {
                            IsPlayer = false;
                        }
                    }
                    catch   
                    {
                        IsPlayer = false;
                    }

                    if (!IsPlayer)
                    {
                        if (IPAddress.TryParse(command[0], out IPAddress address))
                        {
                            switch (address.AddressFamily)
                            {
                                case System.Net.Sockets.AddressFamily.InterNetwork:
                                    IsIP = true;
                                    break;
                            }
                        }

                        if (!IsIP)
                        {
                            UnturnedChat.Say(caller, "User not found!", Color.red);
                            return;

                        }
                        else
                        {
                            if (!SimpleIPBan.Instance.BlacklistedIPs.Contains(address.ToString()))
                            {
                                SimpleIPBan.Instance.AddIP(caller, address.ToString());
                            } else
                            {
                                UnturnedChat.Say(caller, "IP already in blacklist.", Color.red);
                                return;
                            }
                        }


                    }
                    if (IsPlayer)
                    {
                        if (!SimpleIPBan.Instance.BlacklistedIPs.Contains(IP))
                        {
                            SimpleIPBan.Instance.AddPlayerIP(caller, Victim, IP);
                        } else
                        {
                            UnturnedChat.Say(caller, "IP already in blacklist.", Color.red);
                            return;
                        }
                    }

                }
                else
                {
                    UnturnedChat.Say(caller, "Incorrect Usage: /GetIP [Name || SteamID || IP]", Color.red);
                    return;
                }
            }
            catch
            {
                UnturnedChat.Say(caller, "User/IP not found!", Color.red);
            }
        }
    }
}
