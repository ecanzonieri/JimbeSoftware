using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JimbeCore.Repository.Interfaces
{
    public interface IRepository<in TKey, T> : IPersistRepository<T>, IReadOnlyRepository<TKey, T> where T : class
    {
    }
}
