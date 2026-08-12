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
        static GUIStyle valuableStyle;
        static GUIStyle cosmeticStyle;
        void Awake()
        {
            baseStyle = new GUIStyle();
            baseStyle.fontSize = 20;
            baseStyle.alignment = TextAnchor.MiddleCenter;
            baseStyle.normal.textColor = Color.white;
            //
            playerStyle = new GUIStyle(baseStyle);
            playerStyle.normal.textColor = Color.green;
            //
            playerTumbleStyle = new GUIStyle(baseStyle);
            playerTumbleStyle.normal.textColor = new Color(0.2f, 0.6f, 0.2f);
            //
            playerDeadStyle = new GUIStyle(baseStyle);
            playerDeadStyle.normal.textColor = new Color(1f,0.5f,0.5f);
            //
            enemyStyle = new GUIStyle(baseStyle);
            enemyStyle.normal.textColor = Color.red;
            //
            valuableStyle = new GUIStyle(baseStyle);
            valuableStyle.normal.textColor = Color.yellow;
            //
            cosmeticStyle = new GUIStyle(baseStyle);
            cosmeticStyle.normal.textColor = Color.cyan;
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
                foreach(EnemyParent enemy in Enemies.GetAll())
                {
                    GenerateEnemyLabel(enemy);
                }
            }
            if(Funcs.Esp.Valuables)
            {
                foreach(ValuableObject valuable in Valuables.GetAll())
                {
                    GenerateValuableLabel(valuable);
                }
            }
            if (Funcs.Esp.Cosmetics)
            {
                foreach (CosmeticWorldObject cosmetic in Cosmetics.GetAll())
                {
                    GenerateCosmeticLabel(cosmetic);
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
        static void GenerateCosmeticLabel(CosmeticWorldObject cosmetic)
        {
            string text = (cosmetic.rarity == SemiFunc.Rarity.Common) ? "Common" : (cosmetic.rarity == SemiFunc.Rarity.Uncommon) ? "Uncommon" : (cosmetic.rarity == SemiFunc.Rarity.Rare) ? "Rare" : (cosmetic.rarity == SemiFunc.Rarity.UltraRare) ? "Ultra Rare" : "Unknow rarity";
            NotValuableObject nvo = (NotValuableObject)Reflection.GetValue(cosmetic, "notValuableObject");
            GenerateLabel(cosmetic.transform.position, $"{text} \n {(int)Reflection.GetValue(nvo, "healthCurrent")}/{nvo.healthMax}", cosmeticStyle);
        }
        static void GenerateValuableLabel(ValuableObject valuable)
        {
            GenerateLabel(valuable.transform.position, $"{valuable.name} \n {(int)Reflection.GetValue(valuable, "dollarValueOriginal")}$/{(int)Reflection.GetValue(valuable, "dollarValueCurrent")}%", valuableStyle);
        }
        static void GenerateEnemyLabel(EnemyParent enemy)
        {
            if (!(bool)Reflection.GetValue(enemy, "Spawned"))
            {
                return;
            }
            Enemy real = (Enemy)Reflection.GetValue(enemy, "Enemy");
            EnemyHealth health = (EnemyHealth)Reflection.GetValue(real, "Health");
            GenerateLabel(enemy.GetComponentInChildren<Rigidbody>().position+new Vector3(0, 0.5f, 0), $"{enemy.enemyName} \n {health.health}/{(int)Reflection.GetValue(health, "healthCurrent")}", enemyStyle);
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
