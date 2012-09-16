using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Entities;

namespace JimbeTest.Helper
{
    internal class SensorHelper
    {
        internal static IList<Sensor> GetSensorsInfo()
        {
            IList<Sensor> list = new List<Sensor>();
            list.Add(WiFiSensorHelper.GetASensor());
            list.Add(WiFiConnectedSensorHelper.GetASensor());
            return list;
        }

    }
}
