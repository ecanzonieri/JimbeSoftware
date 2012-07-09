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
            var wifiApi = new JimbeWiFi();
            IEnumerable<WifiInterface> interfacelist;
            IEnumerator<WifiInterface> rator;
            int i=0;
            while (i++<120)
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
                        foreach (var MACaddrs in net.Bssids)
                        {
                            Console.WriteLine("\nMAC address: {0}", MACaddrs);
                        }
                    }
                }
                System.Threading.Thread.Sleep(30000);
            }
        }
    }
}
