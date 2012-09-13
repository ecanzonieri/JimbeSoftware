using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Repository.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using NHibernate.Linq;

namespace JimbeTest.NHibernate
{
    [TestClass]
    public class NHibernateTest
    {

        private static ISessionFactory _sessionFactory;
        private static List<WiFiNetwork> networks, connlist;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            var net = new WiFiNetwork("a", 55);
            _sessionFactory = CreateSessionFactory();
            networks = new List<WiFiNetwork>();
            networks.Add(new WiFiNetwork("topolino", 11));
            networks.Add(new WiFiNetwork("gino", 44));
            networks.Add(net);
            connlist = new List<WiFiNetwork>();
            connlist.Add(net);
            
        }

        [TestMethod]
        public void WiFiNetworkTest()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                new PersistenceSpecification<WiFiNetwork>(session)                
                .CheckProperty(c => c.Ssid, "John")
                .CheckProperty(c => c.SignalQuality, 30)
                .VerifyTheMappings();
            }
        }

        [TestMethod]
        public void WiFiConnectedSensorTest()
        {

            using (var session=_sessionFactory.OpenSession())
            {
                new PersistenceSpecification<WiFiConnectedSensor>(session)
                    .CheckProperty(c => c.Weigth, 10.0)
                    .CheckList(c => c.Connected, connlist)
                    .CheckReference(c => c.Location, new Location("casa3",null,null))
                    .VerifyTheMappings();
            }
        }

        [TestMethod]
        public void WiFiSensorTest()
        {
            using (var session = _sessionFactory.OpenSession())
            {

                new PersistenceSpecification<WiFiSensor>(session)
                    .CheckProperty(c => c.Weigth, 10.0)
                    .CheckList(c => c.Networks, networks)
                    .CheckReference(c => c.Location, new Location("casa",null,null))
                    .VerifyTheMappings();
            }
        }

        [TestMethod]
        public void RetrieveWiFiSensorGet()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                Repository<Guid,WiFiSensor> repository = new Repository<Guid, WiFiSensor>(session);
                var wifisensors = repository.All();
                var wifisensor = wifisensors.First();
                if (wifisensor.Weigth!=10.0) Assert.Fail("Weigths disequal");

                IEnumerable<WiFiNetwork> networkcompared = (from net in wifisensor.Networks
                                                        where networks.Any(w => w.EqualsBusiness(net))
                                                        select net);
            if (networkcompared.Count() != networks.Count()) Assert.Fail("networks disequal");
            }
        }

        [TestMethod]
        public void LocationStatisticTest()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                new PersistenceSpecification<LocationStatistic>(session)
                    .CheckProperty(c => c.Start, new DateTime(2001,12,12))
                    .CheckProperty(c => c.End, new DateTime(2004,2,3))
                    .VerifyTheMappings();
            }
        }

        [TestMethod]
        public void LocationTest()
        {
            using (var session = _sessionFactory.OpenSession())
            {


                new PersistenceSpecification<Location>(session)
                    .CheckProperty(c => c.Name, "location1")
                    .CheckList(c => c.StatisticsList, GetStatisticsList())
                    .CheckList(c => c.SensorsList, GetSensorsList())
                    .CheckList(c => c.TasksList, GetTasksList())
                    .VerifyTheMappings();
            }
        }

        private static IList<Statistic> GetStatisticsList()
        {
            var list = new List<Statistic>();
            list.Add(new LocationStatistic()
                         {
                             End=new DateTime(2001,1,1),
                             Start=new DateTime(2002,2,2)
                         });
            list.Add(new LocationStatistic()
                         {
                             End = new DateTime(2003,3,3),
                             Start = new DateTime(2004,4,4)
                         });
            return list;
        }
        
        private static IList<Sensor> GetSensorsList()
        {
            var list = new List<Sensor>();
            list.Add(new WiFiSensor(networks,2,null));
            list.Add(new WiFiConnectedSensor(connlist, 10, null));
            return list;
        } 

        private static IList<Task> GetTasksList()
        {
            var list = new List<Task>();
            list.Add(new StartProcess("pippo"));
            return list;
        }


        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard
                  .UsingFile(Properties.Settings.Default.DbTestPath)
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<Location>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(Properties.Settings.Default.DbTestPath))
            {

                File.Delete(Properties.Settings.Default.DbTestPath);

                // this NHibernate tool takes a configuration (with mapping info in)
                // and exports a database schema from it
                new SchemaExport(config)
                    .Create(false, true);
            }
        }
    }
}
