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


        public delegate void DelLoadUserControl(TCP tcpType);

        public static event DelLoadUserControl OnLoadUserControl;

        public static void LoadUserControlCompleted(TCP tcpType)
        {
            OnLoadUserControl?.Invoke(tcpType);
        }
        public delegate void SendMessageUserControl(string Iserver);

        public static event SendMessageUserControl evtsendmessage;
        public static void SendMessageToUserControl(string Message)
        {
            evtsendmessage?.Invoke(Message);
        }
    }
}
