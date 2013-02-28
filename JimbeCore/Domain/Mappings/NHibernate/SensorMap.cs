using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeService.Domain.Mapping
{
    public class SensorMap : ClassMap<Sensor>
    {
        public SensorMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Weigth);
            Map(x => x.HistorySize);
            References<Location>(x => x.Location).ForeignKey();
        }
    }
}
