using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    public class WiFiNetworkSetMap : ClassMap<WiFiNetworkSet>
    {
        public WiFiNetworkSetMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            HasMany<WiFiNetwork>(x => x.Networks).Cascade.All();
            References<Sensor>(x => x.Sensor);
        }
    }
}
