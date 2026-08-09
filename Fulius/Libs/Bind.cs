using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;
using JetBrains.Annotations;
namespace Fulius
{
    internal class BindInfo
    {
        internal string subTypeName;
        internal string fieldName;
    }
    internal static class Binds
    {
        internal static Collection<Bind> pool;
        internal static Collection<BindInfo> infoPool;
        internal static BindInfo GetBindInfo(string subTypeName, string fieldName)
        {
            BindInfo info = null;
            foreach (BindInfo i in infoPool)
            {
                if (i.subTypeName == subTypeName && i.fieldName == fieldName)
                {
                    info = i;
                    return info;
                }
            }
            BindInfo newInfo = new BindInfo();
            newInfo.subTypeName = subTypeName;
            newInfo.fieldName = fieldName;
            infoPool.Add(newInfo);
            return newInfo;
        }
        internal class Bind 
        {
            internal string subTypeName;
            internal string fieldName;
            internal FieldInfo field;
            internal KeyCode keyCode;
            internal virtual void Process()
            {
                if(keyCode == KeyCode.None) { return; }
                if(Input.GetKeyDown(keyCode))
                {
                    OnPress();
                }
            }
            internal virtual void OnPress()
            {
                if (field == null) { return; }
                if(field.FieldType == typeof(bool))
                {
                    bool curval = (bool)field.GetValue(null);
                    field.SetValue(null, !curval);
                }
            }
            internal static Type GetType(string subTypeName)
            {
                switch (subTypeName)
                {
                    case "Yourself":
                        return typeof(Funcs.Yourself);
                    case "World":
                        return typeof(Funcs.World);
                    default:
                        return null;
                }
            }
            internal static FieldInfo GetField(string subTypeName, string fieldName)
            {
                Type sub = GetType(subTypeName);
                if(sub==null) return null;
                return sub.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            }
            internal Bind(string subTypeName, string fieldName, KeyCode key)
            {
                this.subTypeName = subTypeName;
                this.fieldName = fieldName;
                this.keyCode = key;
                var f = GetField(subTypeName, fieldName);
                this.field = f;
            }
            internal new string ToString()
            {
                return field.Name + " - " + keyCode.ToString();
            }
            
        }
        internal static string GetFilePath()
        {
            return Path.Combine(Assembly.GetExecutingAssembly().Location.Replace(Path.GetFileName(Assembly.GetExecutingAssembly().Location),string.Empty), "binds.xml");
        }
        internal static void OnLoad()
        {
            infoPool = new Collection<BindInfo>();
            pool = new Collection<Bind>();
            Logger.Log("Loading binds file...");
            Logger.Log("Checking binds file...");
            Logger.Log($"Path to binds file: {GetFilePath()}");
            if (!File.Exists(GetFilePath())){ Logger.Log("File doesn't exists! Ignoring!"); return; }
            Logger.Log("File founded! Loading...");
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(GetFilePath());
                foreach(XmlElement bind in doc.GetElementsByTagName("bind"))
                {
                    if (!(bind.HasAttribute("key") || bind.HasAttribute("subTypeName") || bind.HasAttribute("fieldName"))) { Logger.Warn("bind havent 1 or more attribute. skiped!"); continue; }
                    string key = bind.GetAttribute("key");
                    string subTypeName = bind.GetAttribute("subTypeName");
                    string fieldName = bind.GetAttribute("fieldName");
                    int keyInt = -1;
                    if(int.TryParse(key, out keyInt))
                    {
                        Logger.Log("Created bind for key " + ((KeyCode)keyInt).ToString());
                        pool.Add(new Bind(subTypeName, fieldName, (KeyCode)keyInt));
                    }
                    
                }
            }
            catch (Exception e)
            {
                Logger.Error("Failed to load binds file! Error: " + e.Message);
            }
            
            
        }
        internal static void Save()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement main = doc.CreateElement("binds");

            foreach (Bind bind in pool)
            {
                if (bind.field == null || bind.keyCode == KeyCode.None) { continue; }
                XmlElement el = doc.CreateElement("bind");
                el.SetAttribute("key", ((int)bind.keyCode).ToString());
                el.SetAttribute("subTypeName", bind.subTypeName);
                el.SetAttribute("fieldName", bind.fieldName);
                main.AppendChild(el);
            }
            doc.AppendChild(main);
            doc.Save(GetFilePath());
        }
        internal static Bind GetBind(string subTypeName, string fieldName)
        {
            foreach (Bind bind in pool)
            {
                if (bind.subTypeName == subTypeName && bind.fieldName == fieldName)
                {
                    return bind;
                }
            }
            return null;
        }
        internal static Bind GetBind(BindInfo info)
        {
            return GetBind(info.subTypeName, info.fieldName);
        }
        internal static void CreateBind(string subTypeName, string fieldName, KeyCode key)
        {
            BindInfo info = GetBindInfo(subTypeName, fieldName);
            Bind bind = GetBind(info);
            if (bind != null) { Logger.Warn("Bind already exists! replacing..."); bind.keyCode = key; return; }
            pool.Add(new Bind(subTypeName, fieldName, key));
        }
        internal static void CreateBind(BindInfo info, KeyCode key)
        {
            CreateBind(info.subTypeName, info.fieldName, key);
        }
        internal static void RemoveBind(string subTypeName, string fieldName)
        {
            Bind bind = GetBind(subTypeName, fieldName);
            if (bind == null) { Logger.Warn("Bind not found!"); return; }
            pool.Remove(bind);
        }
        internal static void RemoveBind(BindInfo info)
        {
            RemoveBind(info.subTypeName, info.fieldName);
        }
        
    }
}
