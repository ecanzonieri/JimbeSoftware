using System.Runtime.Serialization;

namespace JimbeWCF.DataContracts
{
    [DataContract (Name = "Sensor")]
    [KnownType(typeof(WiFiSensor))]
    [KnownType(typeof(WiFiConnectedSensor))]
    public abstract class Sensor
    {
        [DataMember]
        public double Weigth { get; set; }

        [DataMember]
        public int HistorySize { get; set; }

    }
}