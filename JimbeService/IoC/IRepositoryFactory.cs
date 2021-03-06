﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Repository.Interfaces;

namespace JimbeService.IoC
{
    /// <summary>
    ///     Factory that creates NHibernate repository, this is injected inside the classes that have to create a Repository
    /// </summary>
    public interface IRepositoryFactory
    {
        IRepository<TKey, T> CreateRepository<TKey, T>() where T : class;
    }
}
