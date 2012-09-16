using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public abstract class  Task : Entity<Guid>, ITask
    {
        protected Task()
        {
            Type=TaskType.Spot;
        } 

        protected Task(TaskType type)
        {
            Type = type;
        }

        protected Task(Location location):this()
        {
            Location = location;
        }

        protected Task(Location location, TaskType type)
            : this(type)
        {
            Location = location;
        }

        public enum TaskType
        {
            Spot,
            Periodic,
            Delayed
        }

        #region Implementation of ITask

        public abstract void Execute(object obj);

        public virtual ILocation Location { get; set; }

        public virtual TimeSpan Delay { get; set; }

        public virtual TaskType Type { get; set; }

        public virtual bool Success { get; protected set; }

        #endregion
    }
}
