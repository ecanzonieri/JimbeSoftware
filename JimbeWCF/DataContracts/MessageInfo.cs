using System.Runtime.Serialization;

namespace JimbeWCF.DataContracts
{
    [DataContract (Name= "MessageInfo")]
    public class MessageInfo : Task
    {
        [DataMember]
        public string Message { get; set; }
    }
}