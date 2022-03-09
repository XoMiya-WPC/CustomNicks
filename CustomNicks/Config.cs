using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Interfaces;
namespace CustomNicks
{
    public sealed class Config : IConfig
    {
        [Description("Is the plugin Enabled? - Accepts Bool (Def: true)")]
        public bool IsEnabled { get; set; } = true;
        [Description("Is debugging enabled? - Accepts Bool (Def: false)")]
        public bool EnableDebug { get; set; } = false;

    }
}
