using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name="WiFiNetwork")]
    public class WiFiNetwork
    {
        [DataMember]
        public string Ssid { get; set; }
        
        [DataMember]
        public int SignalQuality { get; set; }

    }
}