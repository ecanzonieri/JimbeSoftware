using System.Collections.Generic;
using System.Linq;
using Jimbe.JimbeWiFi;
using JimbeCore.Domain.Entities;
using TracerX;

namespace JimbeService.Business
{
    public class WiFiManager
    {

        public static readonly double ConnectedWeigth = 2.0;

        public static readonly double NotConnectedWeigth = 1.4;

        private JimbeWiFi.NativeApiVersion _version;

        private static Logger logger = Logger.GetLogger("WiFiManager");

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
            IList<WiFiSensor> sensors = new List<WiFiSensor>();
            try
            {
                using (var wifiApi = new JimbeWiFi(_version))
                {
                    logger.Debug("Getting wlan interfaces list");
                    var interfaceslist = wifiApi.WiFiEnumInterfaces();
                    foreach (WifiInterface wifiInterface in interfaceslist)
                    {
                        logger.Debug("Getting wlan networks list from interface ", wifiInterface.InterfaceGuid, " ", wifiInterface.Description);
                        try
                        {
                            var networkslist = wifiApi.WiFiGetAvailableNetworkList(wifiInterface);

                            IList<WiFiNetwork> networks =
                                (from net in networkslist select new WiFiNetwork(net.Ssid, net.SignalQuality)).ToList();

                            sensors.Add(new WiFiSensor(networks, NotConnectedWeigth, null));
                        } catch (WifiException we)
                        {
                            logger.Error("Error in getting networks from wlan Interface: ", wifiInterface.InterfaceGuid," ", wifiInterface.Description," ", we.Message);
                        }
                    }
                }
            }catch( WifiToManyHandleException we)
            {
                logger.Error(we.Message);
            } catch(WifiException we)
            {
                logger.Error(we.Message);
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
                    logger.Debug("Getting wlan interfaces list");
                    var interfaceslist = wifiApi.WiFiEnumInterfaces();
                    foreach (WifiInterface wifiInterface in interfaceslist.Where(x => x.IsState==WifiInterface.WlanInterfaceState.connected))
                    {
                        logger.Debug("Getting current connected network ", wifiInterface.InterfaceGuid, " ", wifiInterface.Description);
                        try
                        {
                            var network = wifiApi.WiFiGetCurrentConnection(wifiInterface);

                            IList<WiFiNetwork> networks = new List<WiFiNetwork>();

                            networks.Add(new WiFiNetwork(network.Ssid, network.SignalQuality));

                            sensors.Add(new WiFiConnectedSensor(networks, WiFiManager.ConnectedWeigth, null));
                        } catch (WifiException we)
                        {
                            logger.Error("Error in getting Network from wlan Interface: ", wifiInterface.InterfaceGuid, " ", wifiInterface.Description, " ", we.Message);
                        }
                    }
                }
            }
            catch (WifiToManyHandleException we)
            {
                logger.Error(we.Message);
            }
            catch (WifiException we)
            {
                logger.Error(we.Message);
            }
            return sensors;
        }
    }
}
