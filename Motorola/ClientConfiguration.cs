using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorola
{
    internal class ClientConfiguration
    {
        public string IPAddress { get; set; }
        public int PortNumber { get; set; }

        public ClientConfiguration()
        {
            this.PortNumber = int.MinValue;
            this.IPAddress = string.Empty;
        }
    }
}
