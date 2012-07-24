using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jimbe.JimbeCore;
using Jimbe.JimbeWiFi;
using TracerX;

namespace Jimbe
{
    namespace JimbeCore
    {
        public class WiFiConnectedSensor : ISensor
        {
            private readonly WifiNetwork _connected;
            private readonly double _weigth;
            private readonly WifiInterface _wNic;

            private static Logger logger = Logger.GetLogger("WiFiConnectedSensor");

            public WiFiConnectedSensor(WifiInterface wNic,WifiNetwork connected, double weigth)
            {
                _connected = connected;
                _weigth = weigth;
                _wNic = wNic;
            }

            public WifiNetwork Connected
            {
                get { return _connected; }
            }

            #region ISensor Members

            public double Weigth
            {
                get { return _weigth; }
            }

            public WifiInterface WNic
            {
                get { return _wNic; }
            }

            public double GetDistance(ISensor sensor)
            {
                Logger.DefaultBinaryFile.Open();

                if (!(sensor is WiFiConnectedSensor))
                {
                    if (sensor!=null)
                    {
                        logger.Warn("Unable casting ", sensor.GetType(), " to WiFiConnectedSensor");
                        throw new SensorDifferentException("Not able to calculate distance between differents Sensor");
                    }
                    logger.Warn("Unable casting null reference to WiFiConnectedSensor");
                    throw new NullReferenceException();                    
                }

                var wificonnsensor = (WiFiConnectedSensor) sensor;

                if (wificonnsensor.Connected.Ssid != Connected.Ssid) return 0.0;
                return Math.Abs(wificonnsensor.Connected.SignalQuality - Connected.SignalQuality)/100.0;
            }

            public bool Equals(ISensor sensor)
            {
                if (!(sensor is WiFiConnectedSensor))
                    return false;
                WiFiConnectedSensor wiFi = (WiFiConnectedSensor)sensor;
                Guid myGuid = WNic.InterfaceGuid;
                Guid wiFiGuid = wiFi.WNic.InterfaceGuid;
                if (myGuid.Equals(wiFiGuid)) return true;
                return false;
            }

            #endregion
        }
    }
}