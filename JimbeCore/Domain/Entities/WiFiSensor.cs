using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;
using JimbeCore.Exceptions;
using TracerX;

namespace JimbeCore.Domain.Entities
{
    public class WiFiSensor : Sensor
    {
        private static Logger logger = Logger.GetLogger("WiFiSensor");

        public virtual IList<WiFiNetwork> Networks { get; set; }

        public WiFiSensor()
        {
            Networks = new List<WiFiNetwork>();
        }

        public WiFiSensor(IList<WiFiNetwork> networks)
        {
            Networks = networks;
        }

        public WiFiSensor(IList<WiFiNetwork> networks, double weigth, ILocation location) : base (weigth,location)
        {
            Networks = networks;
        }

        #region Overrides of Sensor
        public override double GetDistance(ISensor sensor)
        {
            Logger.DefaultBinaryFile.Open();

            double proximity = 0.0;
            int match = 0;

            if (!(sensor is WiFiSensor))
            {
                if (sensor != null)
                {
                    logger.Warn("Unable casting ", sensor.GetType(), " to WiFiSensor");
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                logger.Warn("Unable casting null reference to WiFiConnectedSensor");
                throw new NullReferenceException();
            }

            var wifisensor = (WiFiSensor) sensor;
            if (wifisensor.Networks == null || Networks == null || !wifisensor.Networks.Any() || !Networks.Any())
                return 0.0;

            foreach (var mynet in Networks)
                foreach (var net in wifisensor.Networks.Where(net => mynet.Ssid == net.Ssid))
                {
                    match++;
                    proximity += Math.Pow((mynet.SignalQuality - net.SignalQuality)/100.0, 2);
                    break;
                }
            int mincount = Math.Min(Networks.Count(), wifisensor.Networks.Count());
            if (match == 0) return 0.0;
            return (1.0 - Math.Sqrt(proximity)/mincount)*match/mincount;
        }
        #endregion


        #region Overrides of Entity<Guid>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            if (ReferenceEquals(this, comparable)) return true;
            var other = comparable as WiFiSensor;
            if (ReferenceEquals(other, null)) return false;
            IEnumerable<WiFiNetwork> networkcompared = (from net in Networks
                                                        where other.Networks.Any(w => w.EqualsBusiness(net))
                                                        select net);
            return networkcompared.Count() == Networks.Count();
        }

        #endregion
    }
}