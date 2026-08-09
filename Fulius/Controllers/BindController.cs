using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius
{
    internal class BindController : MonoBehaviour
    {
        internal static bool bindingNow = false;
        internal static BindInfo bindingInfo;
        internal static BindController instance;
        static GUIStyle labelStyle;
        void Awake()
        {
            instance = this;
            labelStyle = new GUIStyle();
            labelStyle.fontSize = 50;
            labelStyle.normal.textColor = Color.green;
            labelStyle.alignment = TextAnchor.MiddleCenter;
        }
        void OnGUI()
        {
            if (bindingNow)
            {
                if(bindingInfo== null)
                {
                    bindingNow = false;
                    return;
                }
                if(Input.GetKey(Config.rebindKey.Value))
                {
                    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "release rebind key", labelStyle);
                    return;
                }
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), $"Press any key to bind... {Config.bindCancelKey.Value} to cancel, {Config.bindEraseKey.Value} to erase", labelStyle);
                Event cur = Event.current;
                if(cur.isKey)
                {
                    if(cur.keyCode == Config.bindCancelKey.Value)
                    {
                        bindingNow = false;
                        return;
                    }
                    if(cur.keyCode == Config.rebindKey.Value)
                    {
                        return;
                    }
                    if(cur.keyCode == Config.bindEraseKey.Value)
                    {
                        Binds.RemoveBind(bindingInfo);
                        bindingNow = false;
                        return;
                    }
                    if (bindingInfo == null)
                    {
                        bindingNow = false;
                        return;
                    }
                    Binds.CreateBind(bindingInfo, cur.keyCode);
                    Binds.Save();
                    bindingNow = false;
                }
            }
            
            
        }
        void Update()
        {
            if (bindingNow)
            {
                return;
            }
            foreach (Binds.Bind bind in Binds.pool)
            {
                bind.Process();
            }
        }
    }
}
