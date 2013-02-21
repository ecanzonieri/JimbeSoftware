using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name = "Sensor")]
    public class Sensor
    {

        [DataMember]
        public double Weigth { get; set; }

    }
}