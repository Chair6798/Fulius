
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius
{
    internal static class Config
    {
        internal static ConfigEntry<KeyCode> menuKey;
        internal static void Init(ConfigFile Config)
        {
            menuKey = Config.Bind<KeyCode>("Menu", "Toggle", KeyCode.F1);
        }
    }
}
