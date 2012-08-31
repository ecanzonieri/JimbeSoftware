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
        } 

        protected Task(Location location)
        {
            Location = location;
        }

        #region Implementation of ITask

        public abstract bool execute();

        public virtual ILocation Location { get; set; }

        #endregion
    }
}
