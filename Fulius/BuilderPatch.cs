using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fulius.Noclip;
using HarmonyLib;
using UnityEngine;
namespace Fulius.Patchers
{
    [HarmonyPatch(typeof(GameplayManager))]
    internal static class CoreCreator
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void Start_Postfix()
        {
            Logger.Log("Menu builder awaked!");
            if(GameObject.Find("Fulius"))
            {
                Logger.Log("Fulius already created!");
                return;
            }
            Logger.Log("Creating fulius!");
            var go = new GameObject("Fulius");
            GameObject.DontDestroyOnLoad(go);
            go.AddComponent<Menu>();
            go.AddComponent<Controller>();
            go.AddComponent<NoclipController>();
        }
    }
}
