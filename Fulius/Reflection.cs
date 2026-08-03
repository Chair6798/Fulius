using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fulius.Libs
{
    internal static class Reflection
    {
        internal static Dictionary<Type, Dictionary<string, FieldInfo>> reflectedfields = new Dictionary<Type, Dictionary<string, FieldInfo>>();
        internal static FieldInfo GetField(Type type, string name)
        {
            Dictionary<string, FieldInfo> fields;
            if (reflectedfields.TryGetValue(type, out fields))
            {
                FieldInfo field;
                if(fields.TryGetValue(name, out field))
                {
                    return field;
                }
                else
                {
                    field = type.GetField(name);
                    fields.Add(name, field);
                    return field;
                }
            }
            else
            {
                fields = new Dictionary<string, FieldInfo>();
                reflectedfields.Add(type, fields);
                FieldInfo field = type.GetField(name);
                fields.Add(name, field);
                return field;
            }
        }
        internal static FieldInfo GetField(object obj, string name)
        {
            return GetField(obj.GetType(), name);
        }
        internal static object GetValue(object obj, string name)
        {
            FieldInfo field = GetField(obj, name);
            if (field != null)
            {
                return field.GetValue(obj);
            }
            return null;
        }
        internal static void SetValue(object obj, string name, object value)
        {
            FieldInfo field = GetField(obj, name);
            if (field != null)
            {
                field.SetValue(obj, value);
            }
        }
    }
}
