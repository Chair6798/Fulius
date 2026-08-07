using Fulius.Libs;
using HarmonyLib;
using Photon.Pun;
using Steamworks.ServerList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius.Patchers.FuncsPatch
{
    [HarmonyPatch(typeof(PlayerHealth))]
    internal static class PlayerHealthPatch
    {
        [HarmonyPatch("Hurt")]
        [HarmonyPrefix]
        private static bool Hurt_Prefix(PlayerHealth __instance)
        {
            if(PlayersLib.IsLocal(__instance))
            {
                return !Funcs.Yourself.NoDamage;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(PlayerTumble))]
    internal static class PlayerTumblePatch
    {
        [HarmonyPatch("TumbleRequest")]
        [HarmonyPrefix]
        private static bool Tumble_Prefix(PlayerTumble __instance, bool _isTumbling, bool _playerInput)
        {
            if (PlayersLib.IsLocal(__instance))
            {
                if(_isTumbling&&!_playerInput)
                {
                    return !(Funcs.Yourself.NoTumble||Funcs.Yourself.Noclip);
                }
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(PlayerAvatar))]
    internal static class PlayerAvatarPatch
    {
        [HarmonyPatch("PlayerDeathRPC")]
        [HarmonyPrefix]
        private static bool PlayerDeathRPC_Prefix(PlayerAvatar __instance)
        {
            return !(PlayersLib.IsLocal(__instance) && Funcs.Yourself.NoClientDeath);
        }
        [HarmonyPatch("OnPhotonSerializeView")]
        [HarmonyPrefix]
        private static bool OnPhotonSerializeView_Prefix(PlayerAvatar __instance, PhotonStream stream)
        {
            if(stream.IsWriting && Funcs.Yourself.Invisibility)
            {
                stream.SendNext(false);
                stream.SendNext(false);
                stream.SendNext(false);
                stream.SendNext(false);
                stream.SendNext(false);
                stream.SendNext(true);
                stream.SendNext(true);
                stream.SendNext(Vector3.zero);
                stream.SendNext(Vector3.zero);
                stream.SendNext(Vector3.zero);
                stream.SendNext(new Vector3(0, 2000, 0));
                stream.SendNext(Quaternion.identity);
                stream.SendNext(true);
                stream.SendNext(false);
                stream.SendNext(false);
                stream.SendNext(-1);
                stream.SendNext(new Vector3(0, 2000, 0));
                stream.SendNext(false);

                stream.SendNext((int)Reflection.GetValue(__instance, "playerPing"));
                return false;
            }
            return true;
        }
    }
}
