using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius.Libs
{
    internal class GraphicsGenerator
    {
        internal static class Texter
        {
            internal static string PaintString(string str, string color)
            {
                return $"<color={color}>{str}</color>";
            }
        }
        internal static class Imager
        {
            internal static Texture2D GetColoredRect(Vector2Int size, Color color)
            {
                Color[] pack = new Color[size.x * size.y];
                for (int i = 0; i < size.x*size.y; i++)
                {
                    pack[i] = color;
                }
                Texture2D t = new Texture2D(size.x, size.y);
                t.SetPixels(pack);
                t.Apply();
                return t;
            }
        }
        internal static class Styler
        {

            static Dictionary<Color, GUIStyle> coloredRects = new Dictionary<Color, GUIStyle>();
            static Collection<GUIStyle> texts = new Collection<GUIStyle>();
            static void GenerateRect(Color color)
            {
                var style = new GUIStyle();
                style.normal.background = Imager.GetColoredRect(new Vector2Int(2, 2), color);
                coloredRects.Add(color, style);
            }
            internal static GUIStyle Rect(Color color)
            {
                GUIStyle style;
                if (!coloredRects.TryGetValue(color, out style))
                {
                    GenerateRect(color);
                    return Rect(color);
                }
                if(style.normal.background == null)
                {
                    coloredRects.Remove(color);
                    GenerateRect(color);
                    return Rect(color);
                }
                return style;
            }
        }
    }
}
