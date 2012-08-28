using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    public class StatisticMap : ClassMap<Statistic>
    {
        public StatisticMap()
        {
            Id(x => x.Id);
            Map(x => x.Start).Not.Nullable();
            Map(x => x.End);
        }
    }
}
