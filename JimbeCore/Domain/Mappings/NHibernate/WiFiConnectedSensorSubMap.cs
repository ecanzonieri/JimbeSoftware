using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    public class WiFiConnectedSensorSubMap : SubclassMap<WiFiConnectedSensor>
    {
        public WiFiConnectedSensorSubMap()
        {
            HasMany<WiFiNetwork>(x => x.Connected).Cascade.All();
        }
    }
}
