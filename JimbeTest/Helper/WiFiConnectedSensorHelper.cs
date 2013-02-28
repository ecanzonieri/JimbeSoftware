using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Entities;

namespace JimbeTest.Helper
{
    internal class WiFiConnectedSensorHelper
    {

        internal static Sensor GetASensor()
        {
            return new WiFiConnectedSensor()
                                             {
                                                 Datasets = GetNetList(),
                                                 Weigth = 2,
                                                 Location = null
                                             };
        }

        private static List<WiFiNetworkSet> GetNetList()
        {
            IList<WiFiNetwork> nets = new List<WiFiNetwork>();
            nets.Add(new WiFiNetwork("a",70));
            List<WiFiNetworkSet> netlist= new List<WiFiNetworkSet>();
            netlist.Add(new WiFiNetworkSet(nets));
            return netlist;
        }
    }
}
