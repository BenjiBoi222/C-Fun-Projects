using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSorter_1._1
{
    class ServerDevicesObjects
    {
        public string IpAddres {  get; set; }
        public string DeviceName { get; set; }
        public bool IsServer { get; set; } = false;
    }
    
}
