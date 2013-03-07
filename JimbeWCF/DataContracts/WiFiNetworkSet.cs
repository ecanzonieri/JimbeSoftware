using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWCF.DataContracts
{
    [KnownType(typeof(List<WiFiNetwork>))]
    [DataContract (Name = "WiFiNetworkSet")]
    public class WiFiNetworkSet
    {
        [DataMember] 
        public IList<WiFiNetwork> Networks { get; set; }
    }
}