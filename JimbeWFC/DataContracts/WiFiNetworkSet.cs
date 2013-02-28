using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JimbeWFC.DataContracts
{
    [KnownType(typeof(List<WiFiNetwork>))]
    [DataContract (Name = "WiFiNetworkSet")]
    public class WiFiNetworkSet
    {
        [DataMember] 
        public IList<WiFiNetwork> Networks { get; set; }
    }
}