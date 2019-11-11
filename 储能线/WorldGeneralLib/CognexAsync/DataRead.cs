using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Net;
using System.Net.Sockets;

namespace WorldGeneralLib.CognexAsync
{
    class DataRead
    {
        public NetworkStream ns;
        public byte[] msg;
        public DataRead(NetworkStream ns, int buffersize)
        {
            this.ns = ns;
            msg = new byte[buffersize];
        }
    }
}
