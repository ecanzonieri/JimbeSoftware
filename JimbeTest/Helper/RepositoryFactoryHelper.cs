using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Repository.Interfaces;
using JimbeCore.Repository.NHibernate;
using JimbeService.IoC;
using NHibernate;

namespace JimbeTest.Helper
{
    internal class RepositoryFactoryHelper : IRepositoryFactory
    {
        #region Implementation of IRepositoryFactory

        private ISessionFactory _sessionFactory;

        internal RepositoryFactoryHelper(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IRepository<TKey, T> CreateRepository<TKey, T>() where T : class
        {
            return new Repository<TKey, T>(_sessionFactory.OpenSession());
        }

        #endregion
    }
}
