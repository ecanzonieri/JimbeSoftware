using System;
using System.Linq;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Repository.Interfaces;
using JimbeService.IoC;
using JimbeTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JimbeService.Business;

namespace JimbeTest.ServiceTest
{
    /// <summary>
    /// Summary description for StatisticManager
    /// </summary>
    [TestClass]
    public class StatisticManagerTest
    { 

        public StatisticManagerTest()
        {
            
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //Constructor Test
        [TestMethod]
        public void StatisticManagerConstructorTest()
        {
            var location = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0);
            IRepositoryFactory factory = NHibernateHelper.CreateRepositoryFactory();
            StatisticManager statisticManager = new StatisticManager(factory, location);
            Assert.AreEqual(location,statisticManager.Current.Location);
        }

        [TestMethod]
        public void UpdateStatisticTest()
        {
            Location location = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0);
            location.Name = "ServiceManagerTest";
            IRepositoryFactory factory = NHibernateHelper.CreateRepositoryFactory();
            IRepository<Guid, Location> repository = factory.CreateRepository<Guid, Location>();
            
            repository.Add(location);
            

            StatisticManager statisticManager = new StatisticManager(factory, location);
            DateTime expected = DateTime.Now;
            statisticManager.UpdateStatistic(expected);
            Location actualLocation;
            actualLocation = repository.FindBy(x => x.Name == location.Name);
            IStatistic actual =
                actualLocation.Statistics.SingleOrDefault(stat => stat.End == expected);
            Assert.IsNotNull(actual);
       }
    }
}
