using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius.Controllers
{
    internal class ActiveFunctions : MonoBehaviour
    {
        static GUIStyle style;
        void Awake()
        {
            style = new GUIStyle();
            style.alignment = TextAnchor.UpperRight;
            style.normal.textColor = Color.red;
            style.fontSize = 30;
        }
        void OnGUI()
        {
            int i = 0;
            if (Funcs.Yourself.NoDamage)
            {
                GUI.Label(mathPos(i), "No damage", style);
                i++;
            }
            if (Funcs.Yourself.NoClientDeath)
            {
                GUI.Label(mathPos(i), "No client death", style);
                i++;
            }
            if (Funcs.Yourself.InfinityStamina)
            {
                GUI.Label(mathPos(i), "Infinity stamina", style);
                i++;
            }
            if (Funcs.Yourself.Invisibility)
            {
                GUI.Label(mathPos(i), "Invisibility", style);
                i++;
            }
            if (Funcs.Yourself.Noclip)
            {
                GUI.Label(mathPos(i), "Noclip", style);
                i++;
            }
            if (Funcs.Yourself.NoTumble)
            {
                GUI.Label(mathPos(i), "No tumble", style);
                i++;
            }
            if (Funcs.World.FreeCamera)
            {
                GUI.Label(mathPos(i), "Free camera", style);
                i++;
            }
        }
        static Rect mathPos(int i)
        {
            return new Rect(0,5 + (i*40), Screen.width, 40);
        }
    }
}
