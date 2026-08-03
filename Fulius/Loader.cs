using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
namespace Fulius
{
    [BepInPlugin(Data.GUID, Data.Name, Data.Version)]
    internal class Loader : BaseUnityPlugin
    {
        Harmony patch;
        void Awake()
        {
            patch = new Harmony(Data.GUID);
            patch.PatchAll();
            Fulius.Config.Init(Config);
        }
    }
    internal static class Data
    {
        internal const string Name = "Fulius";
        internal const string GUID = "cheat.fulius";
        internal const string Version = "1.0.0";
    }
}
