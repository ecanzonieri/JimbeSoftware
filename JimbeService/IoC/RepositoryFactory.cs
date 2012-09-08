using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Repository.Interfaces;
using Ninject;
using Ninject.Syntax;

namespace JimbeService.IoC
{
    public class RepositoryFactory<TKey, T> : IRepositoryFactory<TKey,T> where T :class 
    {
        #region Implementation of IRepositoryFactory<in TKey,T>


        private IResolutionRoot resolutionRoot;

        public RepositoryFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IRepository<TKey, T> CreateRepository()
        {
           return resolutionRoot.Get<IRepository<TKey,T>>();
        }

        #endregion
    }
}
