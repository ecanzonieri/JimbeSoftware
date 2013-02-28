using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [KnownType(typeof(List<WiFiNetworkSet>))]
    [DataContract (Name = "WiFiConnectedSensor")]
    public class WiFiConnectedSensor : Sensor
    {
        [DataMember]
        public IList<WiFiNetworkSet> Datasets { get; set; }
    }
}