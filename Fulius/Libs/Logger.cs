using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius
{
    internal class Logger
    {
        internal static void Print(object message)
        {
            Console.WriteLine("[Fulius]" + message.ToString());
        }
        internal static void Log(object message)
        {
            Print("[Log]"+message.ToString());
        }
        internal static void Warn(object message)
        {
            Print("[Warn]" + message.ToString());
        }
        internal static void Error(object message)
        {
            Print("[Error]" + message.ToString());
        }
    }
}
