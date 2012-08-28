using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JimbeTest
{
    
    
    /// <summary>
    ///This is a test class for WiFiConnectedSensorTest and is intended
    ///to contain all WiFiConnectedSensorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WiFiConnectedSensorTest
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
        ///A test for WiFiConnectedSensor Constructor
        ///</summary>
        [TestMethod()]
        public void WiFiConnectedSensorConstructorTest()
        {
            WiFiNetwork connected = new WiFiNetwork("Connected", 50);
            double weigth = 5F; // TODO: Initialize to an appropriate value
            WiFiConnectedSensor target = new WiFiConnectedSensor(connected, weigth,null);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetDistance
        ///</summary>
        [TestMethod()]
        public void GetDistanceTest()
        {
            WiFiNetwork connected = new WiFiNetwork("connected", 100); 
            double weigth = 5F; 
            WiFiNetwork connected2= new WiFiNetwork("connected", 50);
            WiFiConnectedSensor target = new WiFiConnectedSensor(connected, weigth,null);
            ISensor sensor = new WiFiConnectedSensor(connected2,weigth,null);
            double expected = 0.5F; 
            double actual;
            actual = target.GetDistance(sensor);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for different connected networks
        /// </summary>
        [TestMethod()]
        public void GetDistanceTest1()
        {
            WiFiNetwork connected = new WiFiNetwork("connected", 100);
            double weigth = 5F;
            WiFiNetwork connected2 = new WiFiNetwork("connected2", 50);
            WiFiConnectedSensor target = new WiFiConnectedSensor(connected, weigth,null);
            ISensor sensor = new WiFiConnectedSensor(connected2, weigth, null);
            double expected = 0F;
            double actual;
            actual = target.GetDistance(sensor);
            Assert.AreEqual(expected, actual);
        }

        

        /// <summary>
        ///A test for Connected
        ///</summary>
        [TestMethod()]
        public void ConnectedTest()
        {
            WiFiNetwork connected = new WiFiNetwork("connected", 100); 
            double weigth = 5F; 
            WiFiConnectedSensor target = new WiFiConnectedSensor(connected, weigth, null); 
            WiFiNetwork actual;
            actual = target.Connected;
            Assert.AreSame(connected,actual);
        }

        /// <summary>
        ///A test for Weigth
        ///</summary>
        [TestMethod()]
        public void WeigthTest()
        {
            WiFiNetwork connected = null; 
            double weigth = 3F; 
            WiFiConnectedSensor target = new WiFiConnectedSensor(connected, weigth, null); 
            double actual;
            actual = target.Weigth;
            Assert.AreEqual(weigth,actual);
        }
    }
}
