using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jimbe.JimbeWiFi;

namespace WiFiApiTest
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<WifiInterface> interfacelist;
            IEnumerator<WifiInterface> rator;
            int i=0;
            while (i++ < 100)
            {
                {

                }
                JimbeWiFi wifiApi = null;
                try
                {
                    wifiApi = new JimbeWiFi();
                }
                catch (WifiToManyHandleException)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                using (wifiApi)
                {
                    interfacelist = wifiApi.WiFiEnumInterfaces();
                    rator = interfacelist.GetEnumerator();
                    while (rator.MoveNext())
                    {
                        Console.WriteLine("Interface Description: {0} GUID {1} State {2}", rator.Current.Description,
                                          rator.Current.InterfaceGuid, rator.Current.IsState);
                        IEnumerable<WifiNetwork> networkslist = wifiApi.WiFiGetAvailableNetworkList(rator.Current);
                        foreach (WifiNetwork net in networkslist)
                        {
                            Console.WriteLine("\nNetwork ssid= {0} Profile= {1} SignalPower={2} Network Type= {3} ",
                                              net.Ssid, net.Profile, net.SignalQuality, net.Type);
                            foreach (var macAddrs in net.Bssids)
                                Console.WriteLine("\nMAC address: {0}", macAddrs);
                        }
                        if (rator.Current.IsState == WifiInterface.WlanInterfaceState.connected)
                        {
                            var currentnetwork = wifiApi.WiFiGetCurrentConnection(rator.Current);
                            Console.WriteLine(
                                "\nCurrent Network ssid= {0} Profile= {1} SignalPower={2} Network Type= {3} ",
                                currentnetwork.Ssid, currentnetwork.Profile, currentnetwork.SignalQuality,
                                currentnetwork.Type);
                            foreach (var macAddrs in currentnetwork.Bssids)
                                Console.WriteLine(" MAC address: {0}", macAddrs);
                        }
                    }
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
    }
}
