using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius.Components
{
    internal class Fake : MonoBehaviour
    {
        public static bool log = false;
        GUIStyle style;
        void Awake()
        {
            style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.red;
            style.alignment = TextAnchor.MiddleCenter;

        }
        void OnGUI()
        {
            //var pos = Camera.
            var pos = Camera.main.WorldToViewportPoint(transform.position);
            if (log)
            {
                Debug.Log(pos);
            }
            if (pos.z<=0)
            {
                return;
            }
            
            GUI.Label(new Rect(pos.x*Screen.width - 200, Screen.height-pos.y*Screen.height - 25, 400, 50), "fake", style);
        }
    }
    
}
