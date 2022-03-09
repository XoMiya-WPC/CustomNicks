using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using GameCore;
using Log = Exiled.API.Features.Log;

namespace CustomNicks.Handlers
{
    class EventHandlers
    {
        private readonly CustomNicks plugin;

        public EventHandlers(CustomNicks plugin)
        {
            this.plugin = plugin;
        }

        public void Verified(VerifiedEventArgs ev)
        {

            if (plugin.NicknameChanges.TryGetValue(ev.Player.UserId, out string nickname))
            {
                string PreviousName = ev.Player.Nickname;
                ev.Player.DisplayNickname = nickname;
                Log.Debug($"Player {PreviousName} / {ev.Player.UserId} has joined and had their name force changed to {nickname}.", plugin.Config.EnableDebug);
            }
            else
                Log.Debug($"Player {ev.Player.Nickname} is not in the Dictionary - ignoring...", plugin.Config.EnableDebug);

        }


        public void EndRound(EndingRoundEventArgs ev)
        {

        }
    }

}
 