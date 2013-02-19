using System;
using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract(Name = "Statistic")]
    public class Statistic
    {
        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }
        
    }
}