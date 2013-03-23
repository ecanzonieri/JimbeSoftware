using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Exceptions;

namespace JimbeCore.Domain.Entities
{
    public class WiFiConnectedSensor : Sensor
    {
        public virtual IList<WiFiNetworkSet> Datasets { get; set; }

        public WiFiConnectedSensor()
        {
            Datasets = new List<WiFiNetworkSet>();
            HistorySize = 10;
        }
        public WiFiConnectedSensor(double weigth, ILocation location): base(weigth,location)
        {
        }
        public WiFiConnectedSensor(IList<WiFiNetworkSet> connected, double weigth, ILocation location)
            : base(weigth, location)
        {
            Datasets = connected;
        }

        #region Overrides of Sensor

        public override double GetDistance(ISensor sensor)
        {
            if (!(sensor is WiFiConnectedSensor))
            {
                if (sensor != null)
                {
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                throw new NullReferenceException();
            }
            var wificonnsensor = (WiFiConnectedSensor) sensor;

            if (!Datasets.Any() || !wificonnsensor.Datasets.Any()) return 0.0;

            if (wificonnsensor.Datasets == null || Datasets == null || !wificonnsensor.Datasets.Any() || !Datasets.Any())
                return 0.0;
            double bestproximity = 0.0;
            foreach (var dataset in Datasets)
                bestproximity = Math.Max(bestproximity, dataset.GetDistance(wificonnsensor.Datasets.First()));
            return bestproximity;
        }

        public override void UpdateSensorDataset(ISensor sensor)
        {
            if (!(sensor is WiFiConnectedSensor))
            {
                if (sensor != null)
                {
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                throw new NullReferenceException();
            }

            var wiFisensor = (WiFiConnectedSensor)sensor;

            for (int i = 0; i < Datasets.Count + wiFisensor.Datasets.Count - HistorySize; i++)
                Datasets.RemoveAt(0);
            foreach (var datasets in wiFisensor.Datasets)
            {
                datasets.Sensor = this;
                Datasets.Insert(Datasets.Count, datasets);
            }
        }

        public override void RemoveInfo(ISensor sensor)
        {
            if (!(sensor is WiFiConnectedSensor))
            {
                if (sensor != null)
                {
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                throw new NullReferenceException();
            }

            var wiFiconnsensor = (WiFiConnectedSensor)sensor;
            double bestproximity = 0.0;
            double tmpproximity;
            WiFiNetworkSet networkSet = null;
            if (Datasets.Count == 1) throw new SensorDatasetException("Sensor " + Id.ToString() + "has one only Dataset. It does not make sense to remove it.");
            foreach (var dataset in Datasets)
                if ((tmpproximity = dataset.GetDistance(wiFiconnsensor.Datasets.First())) > bestproximity)
                {
                    bestproximity = tmpproximity;
                    networkSet = dataset;
                }
            Datasets.Remove(networkSet);
            networkSet.Sensor = null;
        }

        #endregion

        public override bool EqualsBusiness(Model.IBusinessComparable comparable)
        {
            if (ReferenceEquals(this,comparable)) return true;
            var other = comparable as WiFiConnectedSensor;
            if (ReferenceEquals(null,other)) return false;
            if (Datasets.Count() != other.Datasets.Count()) return false;
            var compared = from dataset in Datasets
                           where other.Datasets.Any(set => set.EqualsBusiness(dataset))
                           select dataset;
            if (compared.Count() == Datasets.Count()) return true;
            return false;
        }
    }
}