using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Fulius.Libs;

namespace Fulius
{
    internal class Esp : MonoBehaviour
    {
        public static bool log = false;
        static GUIStyle baseStyle;
        static GUIStyle playerStyle;
        static GUIStyle playerDeadStyle;
        void Awake()
        {
            baseStyle = new GUIStyle();
            baseStyle.fontSize = 20;
            baseStyle.alignment = TextAnchor.MiddleCenter;
            playerStyle = new GUIStyle(baseStyle);
            playerStyle.normal.textColor = Color.green;
            playerDeadStyle = new GUIStyle(baseStyle);
            playerStyle.normal.textColor = new Color(1f,0.2f,0.2f);

        }
        void OnGUI()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {

                var avatar = SemiFunc.PlayerAvatarGetFromPhotonPlayer(p);
                if (avatar != null)
                {
                    GeneratePlayerLabel(avatar);
                }
            }
            
        }
        static void GenerateLabel(Vector3 position, string text, GUIStyle style)
        {
            var pos = Camera.main.WorldToViewportPoint(position);
            if(pos.z<=0)
            {
                return;
            }
            GUI.Label(MakeRect(pos), text, style);
        }
        static void GeneratePlayerLabel(PlayerAvatar avatar)
        {
            Vector3 position;
            int state = 0;
            if (avatar == null)
            {
                return;
            }
            position = avatar.transform.position;
            if ((bool)Reflection.GetValue(avatar, "deadSet"))
            {
                state = 2;
                position = ((PlayerDeathHead)Reflection.GetValue(avatar, "playerDeathHead")).transform.position;
            }
            if ((bool)Reflection.GetValue(avatar, "isTumbling"))
            {
                state = 1;
                position = ((PlayerTumble)Reflection.GetValue(avatar, "tumble")).transform.position;
            }
            GenerateLabel(position, avatar.photonView.Owner.NickName, (state == 2) ? playerDeadStyle : playerStyle);
        }
        static Rect MakeRect(Vector3 pos)
        {
            return new Rect(pos.x * Screen.width - 200, Screen.height - pos.y * Screen.height - 25, 400, 50);
        }
    }
}
