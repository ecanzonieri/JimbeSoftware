using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name = "Sensor")]
    public class WiFiConnectedSensor : Sensor
    {
        [DataMember]
        public IList<WiFiNetwork> Connected { get; set; }
    }
}