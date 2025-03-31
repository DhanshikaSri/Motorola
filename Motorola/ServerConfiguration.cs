using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorola
{
    public class ServerConfiguration
    {
        public string IPAddress { get; set; }
        public int PortNumber { get; set; }

        public ServerConfiguration()
        {
            this.PortNumber = int.MinValue;
            this.IPAddress = string.Empty;
        }
    }
}
