using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;
using JimbeCore.Exceptions;

namespace JimbeCore.Domain.Entities
{
    public class WiFiSensor : Sensor
    {

        public virtual IList<WiFiNetworkSet> Datasets { get; set; }

        public WiFiSensor()
        {
            Datasets = new List<WiFiNetworkSet>();
            HistorySize = 10;
        }

        public WiFiSensor(IList<WiFiNetworkSet> datasets)
        {
            Datasets = datasets;
            HistorySize = 10;
        }

        public WiFiSensor(IList<WiFiNetworkSet> datasets, double weigth, ILocation location)
            : base(weigth, location)
        {
            Datasets = datasets;
            HistorySize = 10;
        }

        #region Overrides of Sensor

        public override double GetDistance(ISensor sensor)
        {
            if (!(sensor is WiFiSensor))
            {
                if (sensor != null)
                {
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                throw new NullReferenceException();
            }

            var wifisensor = (WiFiSensor) sensor;
            if (wifisensor.Datasets == null || Datasets == null || !wifisensor.Datasets.Any() || !Datasets.Any())
                return 0.0;
            double bestproximity=0.0;
            foreach (var dataset in Datasets)
                    bestproximity = Math.Max(bestproximity, dataset.GetDistance(wifisensor.Datasets.First()));
            return bestproximity;
        }

        public override void UpdateSensorDataset(ISensor sensor)
        {
            if (!(sensor is WiFiSensor))
            {
                if (sensor != null)
                {
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                throw new NullReferenceException();
            }

            var wiFisensor = (WiFiSensor) sensor;

            for (int i = 0; i < Datasets.Count+wiFisensor.Datasets.Count-HistorySize; i++)
                Datasets.RemoveAt(0);
            foreach (var datasets in wiFisensor.Datasets)
            {
                datasets.Sensor = this;
                Datasets.Insert(Datasets.Count, datasets);
            }
        }

        public override void RemoveInfo(ISensor sensor)
        {
            if (!(sensor is WiFiSensor))
            {
                if (sensor != null)
                {
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                throw new NullReferenceException();
            }
            var wiFisensor = (WiFiSensor) sensor;

            double bestproximity=0.0;
            double tmpproximity;
            WiFiNetworkSet networkSet=null;
            if (Datasets.Count == 1) throw new SensorDatasetException("Sensor "+ Id.ToString() + "has one only Dataset. It does not make sense to remove it.");
            foreach (var dataset in Datasets)
                if ((tmpproximity = dataset.GetDistance(wiFisensor.Datasets.First())) > bestproximity)
                {
                    bestproximity = tmpproximity;
                    networkSet = dataset;
                }
            Datasets.Remove(networkSet);
            networkSet.Sensor = null;
        }

        #endregion


        #region Overrides of Entity<Guid>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            if (ReferenceEquals(this, comparable)) return true;
            var other = comparable as WiFiSensor;
            if (ReferenceEquals(other, null)) return false;
            if (Datasets.Count() != other.Datasets.Count()) return false;
            var compared = from dataset in Datasets
                           where other.Datasets.Any(set => set.EqualsBusiness(dataset))
                           select dataset;
            if (compared.Count() == Datasets.Count()) return true;
            return false;
        }

        #endregion
    }
}