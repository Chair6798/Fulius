using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
namespace Fulius.Libs
{
    internal static class PlayersLib
    {
        internal static Player GetOwner(GameObject go)
        {
            return go.GetComponent<PhotonView>().Owner;
        }
        internal static bool IsLocal(GameObject o)
        {
            if(o.GetComponent<PlayerAvatar>() != null)
            {
                return (bool)Reflection.GetValue(o.GetComponent<PlayerAvatar>(), "isLocal");
            }
            return false;
        }
        internal static bool IsLocal(PlayerHealth o)
        {
            return IsLocal(o.gameObject);
        }
        internal static bool IsLocal(PlayerAvatar o)
        {
            return IsLocal(o.gameObject);
        }
        internal static bool IsLocal(PhysGrabber o)
        {
            return IsLocal(o.gameObject);
        }
        internal static bool IsLocal(PlayerTumble o)
        {
            return (PlayerTumble)Reflection.GetValue(GetLocalAvatar(), "tumble") == o;
        }
        internal static PlayerAvatar GetAvatar(Player p)
        {
            return SemiFunc.PlayerAvatarGetFromPhotonPlayer(p);
        }
        internal static PlayerAvatar GetLocalAvatar()
        {
            foreach(PlayerAvatar avatar in UnityEngine.Object.FindObjectsOfType<PlayerAvatar>(true))
            {
                if (IsLocal(avatar))
                {
                    return avatar;
                }
            }
            return null;
        }
    }
}
