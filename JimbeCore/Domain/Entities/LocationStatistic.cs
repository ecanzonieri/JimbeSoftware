using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public class LocationStatistic : Statistic
    {
        public virtual ILocation Location { get; set; }

        #region Overrides of Entity<int>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            if (ReferenceEquals(comparable, this)) return true;
            var other = comparable as LocationStatistic;
            if (ReferenceEquals(other, null)) return false;
            return Location.Equals(other.Location) && End == other.End && Start == other.Start; 
        }

        #endregion
    }
}
