using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JimbeCore.Domain.Entities;
using JimbeService.IoC;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Settings = JimbeTest.Properties.Settings;

namespace JimbeTest.Helper
{
    internal class NHibernateHelper
    {
        internal static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard
                        .UsingFile(Settings.Default.DbTestPath)
                )
                .Mappings(m =>
                          m.FluentMappings.AddFromAssemblyOf<Location>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(Settings.Default.DbTestPath))
                File.Delete(Settings.Default.DbTestPath);

                // this NHibernate tool takes a configuration (with mapping info in)
                // and exports a database schema from it
            new SchemaExport(config)
                    .Create(false, true);
        }

        internal static IRepositoryFactory CreateRepositoryFactory()
        {
            return new RepositoryFactoryHelper(CreateSessionFactory());
        }
    }
}
