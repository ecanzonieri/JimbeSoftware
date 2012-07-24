using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TracerX;
using Jimbe.JimbeWiFi;

namespace Jimbe
{
    namespace JimbeCore
    {
        public class WiFiSensor : ISensor
        {
            readonly private IEnumerable<WifiNetwork> _networks;
            readonly private double _weigth;
            readonly private WifiInterface _wNic;

            private static Logger logger = Logger.GetLogger("WiFiSensor");

            public WiFiSensor(WifiInterface wNIC, IEnumerable<WifiNetwork> networks, double weigth)
            {
                _networks = networks;
                _weigth = weigth;
                _wNic = wNIC;
            }

            public IEnumerable<WifiNetwork> Networks
            {
                get { return _networks; }
            } 

            public WifiInterface WNic
            {
                get { return _wNic; }
            }

            #region ISensor Members

            public double Weigth
            {
                get { return _weigth; }
            }

            public double GetDistance(ISensor sensor)
            {
                Logger.DefaultBinaryFile.Open();

                double proximity = 0.0;
                bool match = false;

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

                var wifisensor = (WiFiSensor)sensor;
                if (wifisensor.Networks == null || Networks == null || !wifisensor.Networks.Any() || !Networks.Any()) return 0.0;

                foreach (var mynet in Networks)
                    foreach (var net in wifisensor.Networks.Where(net => mynet.Ssid == net.Ssid))
                    {
                        match = true;
                        proximity += Math.Pow((mynet.SignalQuality - net.SignalQuality) / 100.0, 2);
                        break;
                    }

                if (!match) return 0.0;
                return 1.0 - Math.Sqrt(proximity) / Math.Min(Networks.Count(), wifisensor.Networks.Count());
                
            }

            public bool Equals(ISensor sensor)
            {
                if (!(sensor is WiFiSensor))
                    return false;
                WiFiSensor wiFi = (WiFiSensor) sensor;
                Guid myGuid = WNic.InterfaceGuid;
                Guid wiFiGuid = wiFi.WNic.InterfaceGuid;
                if (myGuid.Equals(wiFiGuid)) return true;
                return false;
            }

            #endregion
        }
    }
}