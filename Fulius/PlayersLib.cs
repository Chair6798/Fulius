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
            var p = GetOwner(o);
            return (p != null && p.IsLocal)||!SemiFunc.IsMultiplayer();
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
        
    }
}
