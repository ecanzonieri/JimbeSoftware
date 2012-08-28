using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    public class TaskStatisticSubMap : SubclassMap<TaskStatistic>
    {
        public TaskStatisticSubMap()
        {
            References<Task>(x => x.Task);
        }
    }
}
