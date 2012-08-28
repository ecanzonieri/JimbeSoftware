using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JimbeCore.Domain.Model
{
    public interface IBusinessComparable
    {
        bool EqualsBusiness(IBusinessComparable comparable);
    }
}
