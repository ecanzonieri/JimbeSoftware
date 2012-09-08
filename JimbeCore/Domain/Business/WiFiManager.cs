using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jimbe.JimbeWiFi;
using JimbeCore.Domain.Entities;
using TracerX;

namespace JimbeCore.Domain.Business
{
    public class WiFiManager
    {
        
        public static readonly double ConnectedWeigth = 10.0;

        public static readonly double NotConnectedWeigth = 1.0;

        private JimbeWiFi.NativeApiVersion _version;

        private static Logger logger = Logger.GetLogger("LocationManager");

        public WiFiManager() : this(JimbeWiFi.NativeApiVersion.windowsXp)
        {
        } 

        public WiFiManager(JimbeWiFi.NativeApiVersion version)
        {
            Logger.DefaultBinaryFile.Open();
            _version = version;
        }

        public IList<WiFiSensor> GetAllWiFiSensorData()
        {
            IList<WiFiSensor> sensors=new List<WiFiSensor>();
            try
            {
                using (var wifiApi = new JimbeWiFi(_version))
                {
                    var interfaceslist = wifiApi.WiFiEnumInterfaces();
                    foreach (WifiInterface wifiInterface in interfaceslist)
                    {
                        var networkslist = wifiApi.WiFiGetAvailableNetworkList(wifiInterface);

                        IList<WiFiNetwork> networks =
                            (from net in networkslist select new WiFiNetwork(net.Ssid, net.SignalQuality)).ToList();

                        sensors.Add(new WiFiSensor(networks));
                    }
                }
            }catch( WifiToManyHandleException we)
            {
                logger.Error(we.Message);
                return null;
            } catch( WifiException we)
            {
                logger.Error(we.Message);
                return null;
            }
            return sensors;
        } 

        public IList<WiFiConnectedSensor> GetAllWiFiConnectedSensorData()
        {
            IList<WiFiConnectedSensor> sensors = new List<WiFiConnectedSensor>();
            try
            {
                using (var wifiApi = new JimbeWiFi(_version))
                {
                    var interfaceslist = wifiApi.WiFiEnumInterfaces();
                    foreach (WifiInterface wifiInterface in interfaceslist.Where(x => x.IsState==WifiInterface.WlanInterfaceState.connected))
                    {
                        var network = wifiApi.WiFiGetCurrentConnection(wifiInterface);

                        IList<WiFiNetwork> networks= new List<WiFiNetwork>();

                        networks.Add(new WiFiNetwork(network.Ssid, network.SignalQuality));

                        sensors.Add(new WiFiConnectedSensor(networks,WiFiManager.ConnectedWeigth,null));
                    }
                }
            }
            catch (WifiToManyHandleException we)
            {
                logger.Error(we.Message);
                return null;
            }
            catch (WifiException we)
            {
                logger.Error(we.Message);
                return null;
            }
            return sensors;
        }
    }
}
