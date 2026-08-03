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
            if(ChatManager.instance != null)
            {
                ChatManager.instance.ForceSendMessage(message);
            }
        }
        internal static bool IsOpen()
        {
            return ChatManager.instance != null && (bool)Reflection.GetValue(ChatManager.instance, "chatActive");
        }
    }
}
