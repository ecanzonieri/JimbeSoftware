using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Repository.Interfaces;
using Ninject;
using Ninject.Syntax;

namespace JimbeService.IoC
{
    public class RepositoryFactory : IRepositoryFactory
    {
        #region Implementation of IRepositoryFactory


        private IResolutionRoot resolutionRoot;

        public RepositoryFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IRepository<TKey, T> CreateRepository<TKey,T>() where T:class 
        {
           return resolutionRoot.Get<IRepository<TKey,T>>();
        }

        #endregion
    }
}
