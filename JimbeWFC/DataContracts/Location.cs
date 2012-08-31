using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name = "Location")]
    [KnownType(typeof(LocationStatistic))]
    [KnownType(typeof(WiFiSensor))]
    [KnownType(typeof(WiFiConnectedSensor))]
    [KnownType(typeof(StartProcess))]
    public class Location
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public IList<Statistic> StatisticsList { get; set; }

        [DataMember]
        public IList<Sensor> SensorsList { get; set; }

        [DataMember]
        public IList<Task> TasksList { get; set; }
 
    }
}