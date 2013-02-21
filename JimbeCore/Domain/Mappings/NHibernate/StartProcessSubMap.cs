using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    class StartProcessSubMap : SubclassMap<StartProcess>
    {
        StartProcessSubMap()
        {
            Map(x => x.ProcessName);
            Map(x => x.Arguments);
        }
    }
}
