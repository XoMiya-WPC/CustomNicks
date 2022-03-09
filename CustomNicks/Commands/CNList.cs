using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CNList
{
    public static class Extensions
    {
        public static IEnumerable<string> Split(this string str, int n)
        {
            for (int i = 0; i < str.Length; i += n)
            {
                yield return str.Substring(i, n);
            }
        }
    }
    [CommandHandler(typeof(RemoteAdminCommandHandler))]

    public class CNList : ICommand
    {
        public string Command { get; } = "cnlist";
        public string[] Aliases { get; } = { "cnl" };
        public string Description { get; } = "Displays a List of the current nicknames";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            //Check if command sender has the correct permissions
            if (!sender.CheckPermission("CustomNicks.list"))
            {
                response = "You do not have permission to run this command";
                return false;
            }

            CustomNicks.CustomNicks.Instance.RefreshNicknames();

            var MaxDict = CustomNicks.CustomNicks.Instance.NicknameChanges.Max(x => x.Key.Length);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"<u><color=#00ffff>{("Player UserID:".PadRight(MaxDict, ' '))}</color></u> | <u><color=#ff00ff>{("Nickname:".PadLeft(5, ' '))}</color></u>");

            foreach (var Nick in CustomNicks.CustomNicks.Instance.NicknameChanges)
            {
                sb.AppendLine($"{Nick.Key.PadRight(MaxDict, ' ')} | {Nick.Value}");
            }
            response = sb.ToString();
            return true;
        }
    }

}