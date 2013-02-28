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
            {
                var query = from mynet in dataset.Networks
                            join net in wifisensor.Datasets.First().Networks
                            on mynet.Ssid equals net.Ssid
                            select new {Quality = net.SignalQuality, MyQuality = mynet.SignalQuality};
                if (query.Any())
                {
                    var proximity = query.Sum(@group => Math.Pow((@group.MyQuality - @group.Quality)/100.0, 2));
                    int mincount = Math.Min(dataset.Networks.Count(), wifisensor.Datasets.First().Networks.Count());
                    bestproximity = Math.Max(bestproximity, (1.0 - Math.Sqrt(proximity)/mincount)*query.Count()/mincount);
                }
            }
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