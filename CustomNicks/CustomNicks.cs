using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Enums;
using System.IO;
using CustomNicks.Handlers;
using Newtonsoft.Json;
using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;

namespace CustomNicks
{
    public class CustomNicks : Plugin<Config>
    {
        public static CustomNicks Instance;
        public override string Name { get; } = "CustomNicks";
        public override string Author { get; } = "XoMiya-WPC";
        public override string Prefix { get; } = "CustomNicks";
        public override Version Version { get; } = new Version("2.0.1");
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);
        public Dictionary<string, string> NicknameChanges { get; private set; } = new Dictionary<string, string>{};
        private EventHandlers server;
        private EventHandlers player;
        public static readonly string exiledPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED"), "Plugins");
        public override void OnEnabled()
        {
            Instance = this;
            server = new EventHandlers(this);
            player = new EventHandlers(this);
            Player.Verified += player.Verified;
            Server.EndingRound += server.EndRound;
            Log.Info("Loading Nicknames");
            RefreshNicknames();
            Log.Info("Custom Nicks has been Enabled! Arigato Gozaimasu!");
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Player.Verified -= player.Verified;
            Server.EndingRound -= server.EndRound;
            server = null;
            player = null;
        }
        internal void RefreshNicknames()
        {
            //Check DIR
            if (!Directory.Exists($"{exiledPath}/CustomNicks"))
            {
                Log.Warn($"{exiledPath}/CustomNicks directory does not exist! Creating...");
                Directory.CreateDirectory($"{exiledPath}/CustomNicks");
            }
            if (!File.Exists($"{exiledPath}/CustomNicks/Nictionary.json"))
            {
                Log.Warn($"{exiledPath}/CustomNicks/Nictionary does not exist! Creating...");
                //File.Create($"{exiledPath}/CustomNicks/Nictionary.yml");
                File.WriteAllText($"{exiledPath}/CustomNicks/Nictionary.json", JsonConvert.SerializeObject(NicknameChanges));
            }
            else
            {
                Log.Debug("Refreshing the Dictionary", Instance.Config.EnableDebug);
                var content = File.ReadAllText($"{exiledPath}/CustomNicks/Nictionary.json");
                NicknameChanges = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            }
        
        }
        internal void UpdateNicknames()
        {
            var content2 = JsonConvert.SerializeObject(NicknameChanges);
            File.WriteAllText($"{exiledPath}/CustomNicks/Nictionary.json", content2);
        }
    }
}
