using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Model;

namespace JimbeTest.Helper
{
    internal class WiFiSensorHelper
    {
        internal static Sensor GetASensor(int seed)
        {
            return new WiFiSensor()
                                    {
                                        Location = null,
                                        Datasets = GetNetList(seed),
                                        Weigth = 1.4
                                    };
        }

        private static List<WiFiNetworkSet> GetNetList(int seed)
        {
            var list=new List<WiFiNetworkSet>();
            IList<WiFiNetwork> nets = new List<WiFiNetwork>();
            if (seed==0)
            {
                nets.Add(new WiFiNetwork("a", 100));
                nets.Add(new WiFiNetwork("b", 80));
                nets.Add(new WiFiNetwork("c", 60));
                nets.Add(new WiFiNetwork("d", 40));
            } else
            {
                nets.Add(new WiFiNetwork("z", 100));
                nets.Add(new WiFiNetwork("x", 80));
                nets.Add(new WiFiNetwork("w", 60));
                nets.Add(new WiFiNetwork("v", 40));
            }
            list.Add(new WiFiNetworkSet(nets));
            return list;
        }
    }
}
