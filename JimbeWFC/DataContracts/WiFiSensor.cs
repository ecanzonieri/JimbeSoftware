using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name = "Sensor")]
    public class WiFiSensor : Sensor
    {
        [DataMember]
        public IList<WiFiNetwork> Networks { get; set; }
    }
}