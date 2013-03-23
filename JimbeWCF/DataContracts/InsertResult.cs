using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JimbeWCF.DataContracts
{
    [DataContract (Name = "LocationInsertException")]
    public class InsertResult
    {
        [DataMember]
        public Location Conflict {get; set;}

        [DataMember]
        public bool Result { get; set; }  

        public InsertResult()
        {
        }

        
    }
}
