using System.Runtime.Serialization;

namespace JimbeWFC.DataContracts
{
    [DataContract (Name= "Task")]
    public class StartProcess : Task
    {
        [DataMember]
        public string ProcessName { get; set; }

        [DataMember]
        public string Arguments { get; set; }

    }
}