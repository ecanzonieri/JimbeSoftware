using System;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public abstract class Sensor : Entity<Guid>, ISensor 
    {
        #region Implementation of ISensor

        public virtual double Weigth { get; set; }

        public virtual int HistorySize { get; set; }

        public virtual ILocation Location { get; set; }

        public abstract double GetDistance(ISensor sensor);

        public abstract void UpdateSensorDataset(ISensor sensor);

        public abstract void RemoveInfo(ISensor sensor);

        protected Sensor()
        {
        }

        protected Sensor(double weigth, ILocation location)
        {
            Weigth = weigth;
            Location = location;
        }

        #endregion
    }
}
