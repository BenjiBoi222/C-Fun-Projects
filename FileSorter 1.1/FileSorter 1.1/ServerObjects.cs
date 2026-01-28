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

        //Only add if the device is a server
        public string SshUsername { get; set; } = "none";
        public string SshPassword { get; set;} = "none";
    }
    
}
