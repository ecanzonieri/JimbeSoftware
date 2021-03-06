﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    public class LocationStatisticSubMap : SubclassMap<LocationStatistic>
    {
        LocationStatisticSubMap()
        {
            References<Location>(x => x.Location);
        }
    }
}
