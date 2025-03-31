using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Motorola
{
    public class TCPManager
    {
        public static bool IsTCPServer = false;

        public static bool IsTCPClient = false;

        public static bool IsClientConnected = false;

        public static bool IsServerConnected = false;

        public static String ConnectedServerIP = string.Empty;

        public static int ConnectedServerPort = int.MinValue;

        public static String ConnectedClientIP = string.Empty;

        public static int ConnectedClientPort = int.MinValue;

        public static Dictionary<string, TcpClient> ConnectedClients = new Dictionary<string, TcpClient>();      

    }

   public enum TCP
    {
        Server,
        Client,
        None
    }

}
