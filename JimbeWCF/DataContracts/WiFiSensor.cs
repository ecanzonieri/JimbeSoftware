using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWCF.DataContracts
{
    [DataContract(Name = "WiFiSensor")]
    [KnownType(typeof(List<WiFiNetwork>))]
    public class WiFiSensor : Sensor
    {
        [DataMember]
        public IList<WiFiNetworkSet> Datasets { get; set; }
    }
}