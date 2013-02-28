using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Model;
using JimbeCore.Repository.NHibernate;
using JimbeTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace JimbeTest.NHibernateTest
{
    [TestClass]
    public class NHibernateTest
    {

        private static ISessionFactory _sessionFactory;
        private static IList<WiFiNetwork> networks, connectednetworks;
        private static List<WiFiNetworkSet> netlist=new List<WiFiNetworkSet>();
        private static List<WiFiNetworkSet> connlist = new List<WiFiNetworkSet>();
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            var net = new WiFiNetwork("a", 55);
            _sessionFactory = NHibernateHelper.CreateSessionFactory();
            networks = new List<WiFiNetwork>();
            networks.Add(new WiFiNetwork("topolino", 11));
            networks.Add(new WiFiNetwork("gino", 44));
            networks.Add(net);
            netlist.Add(new WiFiNetworkSet(networks));
            connectednetworks = new List<WiFiNetwork>();
            connectednetworks.Add(net);
            connlist.Add(new WiFiNetworkSet(connectednetworks));
            
        }

        [TestMethod]
        public void WiFiNetworkTest()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                new PersistenceSpecification<WiFiNetwork>(session)                
                .CheckProperty(c => c.Ssid, "John")
                .CheckProperty(c => c.SignalQuality, 30)
                .CheckReference(c => c.NetworkSet, new WiFiNetworkSet())
                .VerifyTheMappings();
            }
        }

        [TestMethod]
        public void WiFiNetworkSetTest()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                new PersistenceSpecification<WiFiNetworkSet>(session)
                .CheckList(c => c.Networks,networks )
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
                    .CheckList(c => c.Datasets, connlist)
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
                    .CheckList(c => c.Datasets, netlist)
                    .CheckReference(c => c.Location, new Location("casa",null,null))
                    .VerifyTheMappings();
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
            list.Add(new WiFiSensor(netlist,2,null));
            list.Add(new WiFiConnectedSensor(connlist, 10, null));
            return list;
        } 

        private static IList<Task> GetTasksList()
        {
            var list = new List<Task>();
            list.Add(new StartProcess("pippo"));
            return list;
        }
    }
}
