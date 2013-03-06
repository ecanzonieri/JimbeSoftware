using System.Runtime.Serialization;

namespace JimbeWCF.DataContracts
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