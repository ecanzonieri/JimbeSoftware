using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public abstract class Statistic : Entity<int>, IStatistic 
    {
        #region Implementation of IStatistic

        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        
        protected Statistic(){}

        public virtual TimeSpan GetPeriod()
        {
            if (IsFinished())
                return  End - Start;
            return new TimeSpan(0);
        }

        public virtual bool IsFinished()
        {
            return End>Start;
        }

        #endregion
    }
}
