using System;
using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract(Name = "Statistic")]
    [KnownType(typeof(LocationStatistic))]
    public abstract class Statistic
    {
        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }
        
    }
}