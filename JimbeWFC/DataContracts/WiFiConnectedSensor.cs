using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [KnownType(typeof(List<WiFiNetwork>))]
    [DataContract (Name = "WiFiConnectedSensor")]
    public class WiFiConnectedSensor : Sensor
    {
        [DataMember]
        public IList<WiFiNetwork> Connected { get; set; }
    }
}