using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name= "StartProcess")]
    public class StartProcess : Task
    {
        [DataMember]
        public string ProcessName { get; set; }

        [DataMember]
        public string Arguments { get; set; }

    }
}