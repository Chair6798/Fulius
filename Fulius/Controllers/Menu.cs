using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Fulius.Libs;
namespace Fulius
{
    internal class Menu : MonoBehaviour
    {
        internal static Menu instance;
        internal static Vector2Int size = new Vector2Int(1200, 800);
        internal static GUIStyle MenuNameStyle;
        internal static Vector2 SideScrollPosition = Vector2.zero;
        internal static GUIStyle SideBarButtonText;
        internal static Vector2 mainMenuScroll = Vector2.zero;
        internal static string currentMenu;
        internal static bool visible = true;
        void Awake()
        {
            instance = this;
            MenuNameStyle = new GUIStyle();
            MenuNameStyle.fontSize = 80;
            MenuNameStyle.normal.textColor = Color.red;
            MenuNameStyle.alignment = TextAnchor.MiddleCenter;
            SideBarButtonText = new GUIStyle();
            SideBarButtonText.fontSize = 25;
            SideBarButtonText.alignment = TextAnchor.MiddleLeft;
            SideBarButtonText.normal.textColor = Color.white;

        }
        static void SetMouse(bool mode)
        {
            Cursor.visible = mode;
            Cursor.lockState = mode? CursorLockMode.None : CursorLockMode.Locked;
        }
        void OnGUI()
        {
            if(!visible)
            {
                return;
            }
            if(size==Vector2.zero)
            {
                return;
            }
            SetMouse(true);
            if(BindController.bindingNow)
            {
                return;
            }
            Rect windowRect = new Rect((Screen.width - size.x) / 2, (Screen.height - size.y) / 2, size.x, size.y);
            GUI.Box(windowRect, "", GraphicsGenerator.Styler.Rect(Color.black));
            GUI.Label(new Rect(windowRect.position, new Vector2(size.x/3, MenuNameStyle.fontSize+10)), "Fulius", MenuNameStyle);
            int i = 0;
            if (size.y - MenuNameStyle.fontSize + 10>0)
            {
                var sideScrollMenuRect = new Rect(windowRect.position+new Vector2(0, MenuNameStyle.fontSize + 10), new Vector2(size.x / 3, size.y - MenuNameStyle.fontSize - 10));
                GUI.Box(sideScrollMenuRect, "", GraphicsGenerator.Styler.Rect(new Color(0.1f,0.1f,0.1f)));

                int buttonsAmount = 6;

                SideScrollPosition = GUI.BeginScrollView(sideScrollMenuRect, SideScrollPosition, new Rect(0,0,sideScrollMenuRect.width, 5+ buttonsAmount* (30+5)));

                

                SideButton(i, "Yourself", sideScrollMenuRect);
                i++;

                SideButton(i, "Teleport", sideScrollMenuRect);
                i++;

                SideButton(i, "World", sideScrollMenuRect);
                i++;

                SideButton(i, "Valuables", sideScrollMenuRect);
                i++;

                SideButton(i, "Enemies", sideScrollMenuRect);
                i++;

                SideButton(i, "Cosmetic", sideScrollMenuRect);
                i++;

                GUI.EndScrollView();
            }
            mainMenuScroll = GUI.BeginScrollView(new Rect(windowRect.position + new Vector2(windowRect.width / 3,0), new Vector2(windowRect.width/3*2, windowRect.height)), mainMenuScroll, new Rect(0, 0, windowRect.width, windowRect.height));
            i = 0;
            int k = 0;
            switch (currentMenu)
            {
                default:
                    break;
                case "Yourself":
                    
                    BoolOption(i,k, "No damage", ref Funcs.Yourself.NoDamage, bind:Binds.GetBindInfo("Yourself", "NoDamage"));
                    i++;
                    BoolOption(i, k, "No client death", ref Funcs.Yourself.NoClientDeath, bind:Binds.GetBindInfo("Yourself", "NoClientDeath"));
                    i++;
                    BoolOption(i, k, "Infinity stamina", ref Funcs.Yourself.InfinityStamina, bind:Binds.GetBindInfo("Yourself", "InfinityStamina"));
                    i++;
                    BoolOption(i, k, "Invisibility", ref Funcs.Yourself.Invisibility, bind:Binds.GetBindInfo("Yourself", "Invisibility"));
                    i++;
                    BoolOption(i, k, "Noclip", ref Funcs.Yourself.Noclip, bind:Binds.GetBindInfo("Yourself", "Noclip"));
                    i++;
                    BoolOption(i, k, "No tumble", ref Funcs.Yourself.NoTumble, bind:Binds.GetBindInfo("Yourself", "NoTumble"));
                    i++;
                    break;
                case "World":
                    BoolOption(i, k, "Free camera", ref Funcs.World.FreeCamera, bind:Binds.GetBindInfo("World", "FreeCamera"));
                    i++;
                    break;
                case "Teleport":
                    TeleportButton(i, k, "Truck", "truck");
                    i++;
                    TeleportButton(i, k, "Extraction point", "extraction");
                    i++;
                    break;
            }
            GUI.EndScrollView();
        }
        static void SideButton(int i, string text, Rect menuRect)
        {
            Vector2 buttonSize = new Vector2(menuRect.width-10, 30);
            if(GUI.Button(new Rect(new Vector2(5, 5 + i * (buttonSize.y+5)), buttonSize), "", GraphicsGenerator.Styler.Rect(Color.black)))
            {
                currentMenu = text;
            }
            GUI.Label(new Rect(new Vector2(5, 5 + i * (buttonSize.y + 5)), buttonSize), (currentMenu==text)?GraphicsGenerator.Texter.PaintString(text, "green"):text, SideBarButtonText);
        }
        static string GetBoolMark(bool value)
        {
            return value ? "V" : "X";
        }
        
        static void BoolOption(int i, int k, string text, ref bool val, Action<bool> onToggle=null, BindInfo bind = null)
        {
            Rect rect = new Rect(5 + k * (size.x / 3 + 5), 5+i*(30+5), size.x/3, 30);
            if (GUI.Button(rect, "", GraphicsGenerator.Styler.Rect(new Color(0.1f, 0.1f, 0.1f))))
            {
                if (bind != null)
                {
                    if (Input.GetKey(Config.rebindKey.Value))
                    {
                        BindController.bindingInfo = bind;
                        BindController.bindingNow = true;
                        return;
                    }
                }
                
                val=!val;
                onToggle?.Invoke(val);
            }
            if (bind == null)
            {
                GUI.Label(rect, $"{GraphicsGenerator.Texter.PaintString(text, val ? "green" : "red")}({GetBoolMark(val)})", SideBarButtonText);
            }
            else
            {
                GUI.Label(rect, $"{GraphicsGenerator.Texter.PaintString(text, val ? "green" : "red")}({GetBoolMark(val)}) [{((Binds.GetBind(bind)!=null)?(Binds.GetBind(bind).keyCode.ToString()):"none")}]", SideBarButtonText);
            }
            
        }
        static bool Button(int i, int k, string text, Action act=null)
        {
            Rect rect = new Rect(5 + k * (size.x / 3 + 5), 5 + i * (30 + 5), size.x / 3, 30);
            bool b = GUI.Button(rect, "", GraphicsGenerator.Styler.Rect(new Color(0.1f, 0.1f, 0.1f)));
            if (b)
            {
                act?.Invoke();
            }
            GUI.Label(rect, text, SideBarButtonText);
            return b;
        }
        static void TeleportButton(int i, int k, string text, string type)
        {
            Rect rect = new Rect(5 + k * (size.x / 3 + 5), 5 + i * (30 + 5), size.x / 3, 30);
            bool b = GUI.Button(rect, "", GraphicsGenerator.Styler.Rect(new Color(0.1f, 0.1f, 0.1f)));
            GameObject obj = null;
            bool b2 = Objects.GetObject(type, ref obj);
            if (b&&b2)
            {
                LocalPlayer.Teleport(obj.transform.position);
            }
            GUI.Label(rect, $"<color={((b2&&LocalPlayer.ControllerObjectActive())?"white":"yellow")}>{text}</color>", SideBarButtonText);
        }
        void Update()
        {
            if(Input.GetKeyDown(Config.menuKey.Value))
            {
                visible = !visible;
                SetMouse(visible);
            }
        }
    }
}
