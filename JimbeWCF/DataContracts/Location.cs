using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JimbeWCF.DataContracts
{
    [DataContract (Name = "Location")]
    [KnownType(typeof(List<Statistic>))]
    [KnownType(typeof(List<Sensor>))]
    [KnownType(typeof(List<Task>))]
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