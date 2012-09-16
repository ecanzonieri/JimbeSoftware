using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Entities;

namespace JimbeTest.Helper
{
    internal class WiFiSensorHelper
    {
        internal static Sensor GetASensor()
        {
            return new WiFiSensor()
                                    {
                                        Location = null,
                                        Networks = GetNetList('a'),
                                        Weigth = 1.4
                                    };
        }

        private static IList<WiFiNetwork> GetNetList(char c)
        {
            IList<WiFiNetwork> nets = new List<WiFiNetwork>();
            if (c == 'a')
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
            return nets;
        }
    }
}
