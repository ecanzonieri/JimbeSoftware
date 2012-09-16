﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Interfaces;

namespace JimbeTest.Helper
{
    internal class LocationHelper

    {
        internal enum SensorType
        {
            All,
            WiFiConnected,
            WiFi
        };

        internal static Location GenerateLocation(SensorType sensorType)
        {
            IList<Sensor> list = new List<Sensor>();
            switch (sensorType)
            {
                case SensorType.All:
                    list.Add(WiFiSensorHelper.GetASensor());
                    list.Add(WiFiConnectedSensorHelper.GetASensor());
                    break;
                case SensorType.WiFiConnected:
                    list.Add(WiFiConnectedSensorHelper.GetASensor());
                    break;
                case SensorType.WiFi:
                    list.Add(WiFiSensorHelper.GetASensor());
                    break;
            }
            return new Location(list);
        }


    }
}
