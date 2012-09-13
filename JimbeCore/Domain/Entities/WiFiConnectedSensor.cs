using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Exceptions;
using TracerX;

namespace JimbeCore.Domain.Entities
{
    public class WiFiConnectedSensor : Sensor
    {
        public virtual IList<WiFiNetwork> Connected { get; set; }

        private static Logger logger = Logger.GetLogger("WiFiConnectedSensor");

        public WiFiConnectedSensor()
        {
            
        }
        public WiFiConnectedSensor(double weigth, ILocation location): base(weigth,location)
        {
        }
        public WiFiConnectedSensor(IList<WiFiNetwork> connected, double weigth, ILocation location):base(weigth,location)
        {
            Connected = connected;
        }

        #region Overrides of Sensor

        public override double GetDistance(ISensor sensor)
        {
            Logger.DefaultBinaryFile.Open();

            if (!(sensor is WiFiConnectedSensor))
            {
                if (sensor != null)
                {
                    logger.Warn("Unable casting ", sensor.GetType(), " to WiFiConnectedSensor");
                    throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                }
                logger.Warn("Unable casting null reference to WiFiConnectedSensor");
                throw new NullReferenceException();
            }

            var wificonnsensor = (WiFiConnectedSensor) sensor;

            if (!Connected.Any() || !wificonnsensor.Connected.Any()) return 0.0;

            WiFiNetwork net1 = (from n in Connected orderby n.Ssid select n).FirstOrDefault();

            WiFiNetwork net2 = (from n in wificonnsensor.Connected where n.Ssid == net1.Ssid select n).FirstOrDefault();

            if (ReferenceEquals(net2, null)) return 0.0; 

            return (1.0-Math.Abs(net1.SignalQuality - net2.SignalQuality)/100.0);
        }

        #endregion

        public override bool EqualsBusiness(Model.IBusinessComparable comparable)
        {
            if (ReferenceEquals(this,comparable)) return true;
            var other = comparable as WiFiConnectedSensor;
            if (ReferenceEquals(null,other)) return false;
            IEnumerable<WiFiNetwork> networkcompared = (from net in Connected
                                                        where other.Connected.Any(w => w.EqualsBusiness(net))
                                                        select net);
            return networkcompared.Count() == Connected.Count();
        }
    }
}