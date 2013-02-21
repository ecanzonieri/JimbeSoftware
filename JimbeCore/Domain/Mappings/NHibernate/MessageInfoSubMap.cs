using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using JimbeCore.Domain.Entities;

namespace JimbeCore.Domain.Mappings.NHibernate
{
    class MessageInfoSubMap : SubclassMap<MessageInfo>
    {
        MessageInfoSubMap()
        {
            Map(x => x.Message);
        }
    }
}
