using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorola
{
    public static class ParentMessageHandler
    {

        public delegate void LogEventHandler(string message);

        public static event LogEventHandler OnLog;

        public static void Log(string message)
        {
            OnLog.Invoke("{DateTime.Now:HH:mm:ss} - {message}");
        }


        public delegate void DelLoadUserControl(bool Iserver);

        public static event DelLoadUserControl OnloadUserControl;
        public static void LoadUserControlCompleted(bool isServer)
        {
            OnloadUserControl?.Invoke(isServer);
        }

        public static void RemoveUserControlCompleted(bool isServer)
        {
            OnloadUserControl?.Invoke(isServer);
        }


        public delegate void SendMessageUserControl(string Iserver);

        public static event SendMessageUserControl evtsendmessage;
        public static void SendMessageToUserControl(string Message)
        {
            evtsendmessage?.Invoke(Message);
        }
    }
}
