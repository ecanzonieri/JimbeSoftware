using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Mappings
{
    public class WiFiNetworkMap : ClassMap<WiFiNetwork>
    {
        public WiFiNetworkMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Ssid).Not.Nullable();
            Map(x => x.SignalQuality);
            References<WiFiNetworkSet>(x => x.NetworkSet).ForeignKey();
        }
    }
}
