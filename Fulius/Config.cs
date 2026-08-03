
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
        //noclip binds
        internal static ConfigEntry<KeyCode> noclipForward;
        internal static ConfigEntry<KeyCode> noclipBackward;
        internal static ConfigEntry<KeyCode> noclipRight;
        internal static ConfigEntry<KeyCode> noclipLeft;
        internal static ConfigEntry<KeyCode> noclipUp;
        internal static ConfigEntry<KeyCode> noclipDown;
        internal static ConfigEntry<KeyCode> noclipFaster;
        internal static ConfigEntry<float> noclipSpeed;

        internal static void Init(ConfigFile Config)
        {
            menuKey = Config.Bind<KeyCode>("Menu", "Toggle", KeyCode.F1);
            // noclip binds
            noclipForward = Config.Bind<KeyCode>("Noclip", "Forward", KeyCode.W);
            noclipBackward = Config.Bind<KeyCode>("Noclip", "Backward", KeyCode.S);
            noclipRight = Config.Bind<KeyCode>("Noclip", "Right", KeyCode.D);
            noclipLeft = Config.Bind<KeyCode>("Noclip", "Left", KeyCode.A);
            noclipUp = Config.Bind<KeyCode>("Noclip", "Up", KeyCode.Space);
            noclipDown = Config.Bind<KeyCode>("Noclip", "Down", KeyCode.LeftControl);
            noclipFaster = Config.Bind<KeyCode>("Noclip", "Faster", KeyCode.LeftShift);
            noclipSpeed = Config.Bind<float>("Noclip", "Speed", 10f);
        }
    }
}
