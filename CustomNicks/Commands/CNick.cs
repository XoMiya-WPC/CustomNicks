using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using Exiled.Permissions.Extensions;

namespace CustomNicks
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]

    public class CNick : ICommand
    {
        public string Command { get; } = "cnick";
        public string[] Aliases { get; } = { "CustomNicks" };
        public string Description { get; } = "Parent Command for Custom Nicks";
        public const string UsageHelp = "Usage: cnick (add/remove/update/lookup) (userid/playerid) (add/update: newnickname)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            //Check if command sender has the correct permissions
            if (!sender.CheckPermission("customnicks.cnick"))
            {
                response = "You do not have permission to run this command!";
                return false;
            }
            //Check if the command sender has included atleast 2 arguments, the add/remove/update and the ID
            if (arguments.Count < 2)
            {
                response = UsageHelp;
                return false;
            }
            Log.Debug("Argument count was above 2 or above - proceeding", CustomNicks.Instance.Config.EnableDebug);
            //deterine whether the command includes a player id or user id
            string userId;
            Player nickchangePlayer = Player.Get(arguments.At(1));
            if (arguments.At(1).Contains("@"))
            {
                userId = arguments.At(1);
            }
            else
            {
                if (!int.TryParse(arguments.At(1), out int playerId))
                {
                    response = $"{arguments.At(1)} is not a valid Player ID!\n\n{UsageHelp}";
                    return false;
                }
                if (nickchangePlayer != null)
                {
                    userId = nickchangePlayer.UserId;
                }
                else
                {
                    response = $"A player with Player ID {playerId} cannot be found on this server.\n\n{UsageHelp}";
                    return false;
                }
            }


            //switch to determine choice
            switch (arguments.At(0).ToUpper())
            {
                case "ADD":
                    {
                        //Check if the new nickname is empty
                        if (arguments.Count < 3)
                        {
                            response = UsageHelp;
                            return false;
                        }
                        else
                        {
                            CustomNicks.Instance.RefreshNicknames();
                            Log.Debug("Case is Add", CustomNicks.Instance.Config.EnableDebug);
                            //Check if the Dictionary contains that player
                            if (CustomNicks.Instance.NicknameChanges.ContainsKey(userId))
                            {
                                response = $"Failed to add {userId}'s nickname - Reason: Player is already in dictionary - Use UPDATE";
                                return false;
                            }
                            else
                            {
                                //Add players nickname
                                CustomNicks.Instance.NicknameChanges.Add(userId, arguments.At(2));
                                nickchangePlayer.DisplayNickname = arguments.At(2);
                                response = $"Succesfully added {arguments.At(2)} to the dictionary!";
                                CustomNicks.Instance.UpdateNicknames();
                                return true;
                            }
                        }
                    }

                case "UPDATE":
                    {
                        //Check if the new nickname is empty
                        if (arguments.Count <3)
                        {
                            response = UsageHelp;
                            return false;
                        }
                        else
                        {
                            CustomNicks.Instance.RefreshNicknames();
                            Log.Debug("Case is Update", CustomNicks.Instance.Config.EnableDebug);
                            //Check if the Dictionary contains that player
                            if (CustomNicks.Instance.NicknameChanges.ContainsKey(userId))
                            {
                                //Change players nickname
                                CustomNicks.Instance.NicknameChanges[userId] = arguments.At(2);
                                nickchangePlayer.DisplayNickname = arguments.At(2);
                                response = $"Succesfully updated {userId}'s nickname change to {arguments.At(2)}";
                                CustomNicks.Instance.UpdateNicknames();
                                return true;
                            }
                            else
                            {
                                response = $"Failed to update {userId}'s nickname change - Reason: Player is not in dictionary!";
                                return false;
                            }
                        }
                    }

                case "REMOVE":
                    {
                        CustomNicks.Instance.RefreshNicknames();
                        Log.Debug("Case is Remove", CustomNicks.Instance.Config.EnableDebug);
                        //Check if the Dictionary contains that player
                        if (CustomNicks.Instance.NicknameChanges.ContainsKey(userId))
                        {
                            //Remove players presence in the Dictionary
                            CustomNicks.Instance.NicknameChanges.Remove(userId);
                            nickchangePlayer.DisplayNickname = null;
                            response = $"Succesfully removed {userId} from the dictionary!";
                            CustomNicks.Instance.UpdateNicknames();
                            return true;
                        }
                        else
                        {

                            response = $"Failed to remove {userId}'s nickname change - Reason: Player is not in the dictionary!";
                            return false;
                        }
                    }
                case "LOOKUP":
                    {
                        CustomNicks.Instance.RefreshNicknames();
                        Log.Debug("Case is Lookup", CustomNicks.Instance.Config.EnableDebug);
                        if (CustomNicks.Instance.NicknameChanges.ContainsKey(userId))
                        {
                            CustomNicks.Instance.NicknameChanges.TryGetValue(userId, out string value);
                            response = $"Player {userId}'s nickname is set to <b><color=#ff00ff>{value}</color></b>";
                            return true;
                        }
                        else
                        {
                            response = $"Player {userId} was not found in the dictionary!";
                            return false;
                        }
                    }
                default:
                    //When they fuckup choosing add / update / remove
                    Log.Debug("Case is Default", CustomNicks.Instance.Config.EnableDebug);
                    response = UsageHelp;
                    return false;

            }
            
        }
    }
}