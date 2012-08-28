using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public class TaskStatistic : Statistic
    {
        public virtual ITask Task { get; set; }

        #region Overrides of Entity<int>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            if (ReferenceEquals(comparable, this)) return true;
            var other = comparable as TaskStatistic;
            if (ReferenceEquals(other, null)) return false;
            return Task.Equals(other.Task) && End == other.End && Start == other.Start;
        }

        #endregion
    }
}
