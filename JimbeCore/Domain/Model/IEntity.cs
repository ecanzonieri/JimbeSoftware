using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JimbeCore.Domain.Model
{
    public interface IEntity<T>
    {
        T Id { get; }

        bool IsTransient();
    }
}

