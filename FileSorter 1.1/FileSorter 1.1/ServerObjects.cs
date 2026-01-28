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

        /// <summary>
        /// Expandable, it stores what kind of device there is so there can be any type
        /// </summary>
        public string DeviceType { get; set; } = "Device";

        //Only add if the device is a server
        public string SshUsername { get; set; } = "none";
        public string SshPassword { get; set;} = "none";
    }
    
}
