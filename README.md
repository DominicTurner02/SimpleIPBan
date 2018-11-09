# SimpleIPBan
## Adds the ability to blacklist certain IPs from joining your Server. 


**Commands**:

/GetIP [Name or SteamID] - Gets the IP of a User.
/BanIP [Name or SteamID or IP] - Adds an IP to the blacklist.
/IPList - Logs all blacklistted IPs to the console
/IPReload - Refreshes the blacklisted IP JSON Array.


**Configuration:**

KickOnIPBan - If true, kicks the User when you /BanIP them.
KickReason - Allows you to add your own Kick Reason (Default: "You are IP Banned!")
