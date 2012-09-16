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
                                                 Connected = GetNetList(),
                                                 Weigth = 2,
                                                 Location = null
                                             };
        }

        private static IList<WiFiNetwork> GetNetList()
        {
            IList<WiFiNetwork> nets = new List<WiFiNetwork>();
            nets.Add(new WiFiNetwork("a",70));
            return nets;
        }
    }
}
