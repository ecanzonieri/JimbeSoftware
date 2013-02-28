using System;
using JimbeCore.Domain.Interfaces;
using JimbeTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JimbeTest.EntitiesTest
{
    
    
    /// <summary>
    ///This is a test class for ILocationTest and is intended
    ///to contain all ILocationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ILocationTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion



        /// <summary>
        ///A test for Description
        ///</summary>
        [TestMethod()]
        public void DescriptionTest()
        {
            ILocation target = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0); // TODO: Initialize to an appropriate value
            string expected = "pippo"; // TODO: Initialize to an appropriate value
            string actual;
            target.Description = expected;
            actual = target.Description;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            ILocation target = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0); // TODO: Initialize to an appropriate value
            string expected = "Pippo"; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for GetLocationAffinity
        /// </summary>
        [TestMethod()]
        public void GetLocationAffinityTest()
        {
            ILocation target = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0);
            double expected = 1.0;
            ILocation location = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0);
            var actual = target.GetLocationAffinity(target);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetLocationAffinityTest2()
        {
            ILocation target = LocationHelper.GenerateLocation(LocationHelper.SensorType.WiFi,0);
            double expected = 0.4;
            ILocation location = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0);
            var actual = target.GetLocationAffinity(location);
            Assert.AreEqual(expected,actual,0.02);
        }

        [TestMethod()]
        public void GetLocationAffinityTest3()
        {
            ILocation target = LocationHelper.GenerateLocation(LocationHelper.SensorType.WiFiConnected,0);
            double expected = 0.58;
            ILocation location = LocationHelper.GenerateLocation(LocationHelper.SensorType.All,0);
            var actual = target.GetLocationAffinity(location);
            Assert.AreEqual(expected, actual, 0.02);
        }

        [TestMethod()]
        public void GetLocationAffinityTest4()
        {
            ILocation target = LocationHelper.GenerateLocation(LocationHelper.SensorType.WiFiConnected,0);
            double expected = 0.0;
            ILocation location = LocationHelper.GenerateLocation(LocationHelper.SensorType.WiFi,0);
            var actual = target.GetLocationAffinity(location);
            Assert.AreEqual(expected, actual);
        }
        ///<summary>
        /// Test UpdateLocationSensors
        /// </summary>
        [TestMethod()]
        public void UpdateLocationSensorsTest()
        {
            ILocation target = LocationHelper.GenerateLocation(LocationHelper.SensorType.WiFi,0);
            ILocation loc2 = LocationHelper.GenerateLocation(LocationHelper.SensorType.WiFi, 1);
            var affinity = target.GetLocationAffinity(loc2);
            Assert.AreNotEqual(1.0,affinity);
            target.UpdateLocationSensors(loc2);
            affinity = target.GetLocationAffinity(loc2);
            Assert.AreEqual(1.0,affinity);
        }

    }
}
