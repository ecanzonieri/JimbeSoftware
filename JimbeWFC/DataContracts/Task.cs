﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace JimbeWFC.DataContracts
{
    [DataContract(Name = "Task")]
    [KnownType(typeof(StartProcess))]
    [KnownType(typeof(MessageInfo))]
    public abstract class Task
    {
        [DataContract(Name= "TaskType")]
        public enum TaskType
        {
            [EnumMember]
            Spot,
            [EnumMember]
            Periodic,
            [EnumMember]
            Delayed
        }

        [DataMember]
        public TimeSpan Delay { get; set; }

        [DataMember]
        public TaskType Type { get; set; }
    }
}