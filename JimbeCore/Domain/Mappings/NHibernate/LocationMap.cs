using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    public class LocationMap : ClassMap<Location>
    {
        public LocationMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable().Unique();
            Map(x => x.Description);
            HasMany<Sensor>(x => x.SensorsList).Cascade.All();
            HasMany<Task>(x => x.TasksList).Cascade.All();
            HasMany<LocationStatistic>(x => x.StatisticsList).Cascade.All();
        }
    }
}
