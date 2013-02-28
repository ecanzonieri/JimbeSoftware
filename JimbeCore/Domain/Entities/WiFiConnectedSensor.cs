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

            WiFiNetwork net1 = (from n in Datasets.First().Networks orderby n.Ssid select n).FirstOrDefault();

            WiFiNetwork net2 = (from n in wificonnsensor.Datasets.First().Networks where n.Ssid == net1.Ssid select n).FirstOrDefault();

            if (ReferenceEquals(net2, null)) return 0.0; 

            return (1.0-Math.Abs(net1.SignalQuality - net2.SignalQuality)/100.0);
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