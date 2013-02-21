using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name= "MessageInfo")]
    public class MessageInfo : Task
    {
        [DataMember]
        public string Message { get; set; }
    }
}