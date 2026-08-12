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
        static GUIStyle playerTumbleStyle;
        static GUIStyle playerDeadStyle;
        static GUIStyle enemyStyle;
        void Awake()
        {
            baseStyle = new GUIStyle();
            baseStyle.fontSize = 20;
            baseStyle.alignment = TextAnchor.MiddleCenter;
            baseStyle.normal.textColor = Color.white;
            playerStyle = new GUIStyle(baseStyle);
            playerStyle.normal.textColor = Color.green;
            playerTumbleStyle = new GUIStyle(baseStyle);
            playerTumbleStyle.normal.textColor = new Color(0.2f, 0.6f, 0.2f);
            playerDeadStyle = new GUIStyle(baseStyle);
            playerDeadStyle.normal.textColor = new Color(1f,0.5f,0.5f);
            enemyStyle = new GUIStyle(baseStyle);
            enemyStyle.normal.textColor = Color.red;

        }
        void OnGUI()
        {
            if(Funcs.Esp.Players&& SemiFunc.IsMultiplayer())
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
            if(Funcs.Esp.Enemies)
            {
                foreach(EnemyParent enemy in UnityEngine.Object.FindObjectsByType<EnemyParent>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    GenerateEnemyLabel(enemy);
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
        static void GenerateEnemyLabel(EnemyParent enemy)
        {
            if (!(bool)Reflection.GetValue(enemy, "Spawned"))
            {
                return;
            }
            GenerateLabel(enemy.GetComponentInChildren<Rigidbody>().position+new Vector3(0, 0.5f, 0), enemy.enemyName, enemyStyle);
        }
        static void GeneratePlayerLabel(PlayerAvatar avatar)
        {
            Vector3 position;
            int state = 0;
            if (avatar == null)
            {
                return;
            }
            if ((bool)Reflection.GetValue(avatar, "isCrouching"))
            {
                position = avatar.transform.position + new Vector3(0, 0.5f, 0);
            }
            else
            {
                if ((bool)Reflection.GetValue(avatar, "isCrawling"))
                {
                    position = avatar.transform.position + new Vector3(0, 0.2f, 0);
                }
                else
                {
                    position = avatar.transform.position + new Vector3(0, 1, 0);
                }
                
            }
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
            GenerateLabel(position, $"{avatar.photonView.Owner.NickName} \n {(int)Reflection.GetValue(avatar.playerHealth,"health")}/{(int)Reflection.GetValue(avatar.playerHealth, "maxHealth")}", (state == 2) ? playerDeadStyle:(state==1) ? playerTumbleStyle : playerStyle);
        }
        static Rect MakeRect(Vector3 pos)
        {
            return new Rect(pos.x * Screen.width - 200, Screen.height - pos.y * Screen.height - 25, 400, 50);
        }
    }
}
