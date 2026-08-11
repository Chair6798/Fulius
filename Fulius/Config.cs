
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
        internal static ConfigEntry<KeyCode> rebindKey;
        internal static ConfigEntry<KeyCode> bindCancelKey;
        internal static ConfigEntry<KeyCode> bindEraseKey;
        //noclip binds
        internal static ConfigEntry<KeyCode> noclipForward;
        internal static ConfigEntry<KeyCode> noclipBackward;
        internal static ConfigEntry<KeyCode> noclipRight;
        internal static ConfigEntry<KeyCode> noclipLeft;
        internal static ConfigEntry<KeyCode> noclipUp;
        internal static ConfigEntry<KeyCode> noclipDown;
        internal static ConfigEntry<KeyCode> noclipFaster;
        internal static ConfigEntry<float> noclipSpeed;
        //camera binds
        internal static ConfigEntry<KeyCode> cameraForward;
        internal static ConfigEntry<KeyCode> cameraBackward;
        internal static ConfigEntry<KeyCode> cameraRight;
        internal static ConfigEntry<KeyCode> cameraLeft;
        internal static ConfigEntry<KeyCode> cameraUp;
        internal static ConfigEntry<KeyCode> cameraDown;
        internal static ConfigEntry<KeyCode> cameraFaster;
        internal static ConfigEntry<float> cameraSpeed;
        internal static ConfigEntry<float> cameraSens;
        internal static void Init(ConfigFile Config)
        {
            menuKey = Config.Bind<KeyCode>("Menu", "Toggle", KeyCode.F1);
            rebindKey = Config.Bind<KeyCode>("Menu", "Rebind", KeyCode.LeftControl);
            bindCancelKey = Config.Bind<KeyCode>("Menu", "Cancel", KeyCode.Escape);
            bindEraseKey = Config.Bind<KeyCode>("Menu", "Erase", KeyCode.Backspace);
            // noclip binds
            noclipForward = Config.Bind<KeyCode>("Noclip", "Forward", KeyCode.W);
            noclipBackward = Config.Bind<KeyCode>("Noclip", "Backward", KeyCode.S);
            noclipRight = Config.Bind<KeyCode>("Noclip", "Right", KeyCode.D);
            noclipLeft = Config.Bind<KeyCode>("Noclip", "Left", KeyCode.A);
            noclipUp = Config.Bind<KeyCode>("Noclip", "Up", KeyCode.Space);
            noclipDown = Config.Bind<KeyCode>("Noclip", "Down", KeyCode.LeftControl);
            noclipFaster = Config.Bind<KeyCode>("Noclip", "Faster", KeyCode.LeftShift);
            noclipSpeed = Config.Bind<float>("Noclip", "Speed", 10f);
            // camera binds
            cameraForward = Config.Bind<KeyCode>("Camera", "Forward", KeyCode.W);
            cameraBackward = Config.Bind<KeyCode>("Camera", "Backward", KeyCode.S);
            cameraRight = Config.Bind<KeyCode>("Camera", "Right", KeyCode.D);
            cameraLeft = Config.Bind<KeyCode>("Camera", "Left", KeyCode.A);
            cameraUp = Config.Bind<KeyCode>("Camera", "Up", KeyCode.Space);
            cameraDown = Config.Bind<KeyCode>("Camera", "Down", KeyCode.LeftControl);
            cameraFaster = Config.Bind<KeyCode>("Camera", "Faster", KeyCode.LeftShift);
            cameraSpeed = Config.Bind<float>("Camera", "Speed", 1f);
            cameraSens = Config.Bind<float>("Camera", "Sensivity", 1f);
        }
    }
}
