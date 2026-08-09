using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fulius.Libs
{
    internal static class Chat
    {
        internal static void Send(string message)
        {
            if (ChatManager.instance != null)
            {
                ChatManager.instance.ForceSendMessage(message);
            }
        }
        internal static bool IsOpen()
        {
            if (ChatManager.instance == null)
            {
                return false;

            }
            else
            {
                var val = Reflection.GetValue(ChatManager.instance, "chatActive");
                if (val is bool b)
                    return b;
                return false;
            }
        }
    }
}
