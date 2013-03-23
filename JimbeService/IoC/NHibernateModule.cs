using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JimbeCore.Domain.Entities;
using JimbeCore.Repository.Interfaces;
using JimbeCore.Repository.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace JimbeService.IoC
{
    public class NHibernateModule : NinjectModule
    {



        #region Overrides of NinjectModule

        public override void Load()
        {
            Bind<ISessionFactory>().ToMethod(x => CreateSessionFactory()).InSingletonScope();
            Bind<ISession>().ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession());
            Bind<IRepository<Guid, Location>>().To<Repository<Guid, Location>>();
            Bind<IRepository<int, LocationStatistic>>().To<Repository<int, LocationStatistic>>();
            Bind<IRepositoryFactory>().ToFactory();
        }

        #endregion

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard
                  .UsingFile(Properties.Settings.Default.DBPath)
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<Location>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            File.Delete(Properties.Settings.Default.DBPath);
            if (!File.Exists(Properties.Settings.Default.DBPath))
            {
                new SchemaExport(config)
                    .Create(false, true);
                //File.Delete(Properties.Settings.Default.DBPath);
            }
            
        }

    }
}
