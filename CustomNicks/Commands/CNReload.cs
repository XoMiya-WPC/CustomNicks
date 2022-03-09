using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using Exiled.Permissions.Extensions;

namespace CNReload
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]

    public class CNReload : ICommand
    {
        public string Command { get; } = "cnreload";
        public string[] Aliases { get; } = { "cnr" };
        public string Description { get; } = "Reloads the Custom Nicks Plugin";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            //Check if command sender has the correct permissions
            if (!sender.CheckPermission("CustomNicks.reload"))
            {
                response = "You do not have permission to run this command";
                return false;
            }

            CustomNicks.CustomNicks.Instance.RefreshNicknames();
            response = "Successfully Reloaded Nicknames";
            return true;
        }
    }
}