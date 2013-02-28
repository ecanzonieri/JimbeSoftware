using System.Collections.Generic;
using JimbeCore.Domain.Entities;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JimbeTest.EntitiesTest
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



        private List<WiFiNetworkSet> GetConnectedList()
        {
            List<WiFiNetworkSet> netlist=new List<WiFiNetworkSet>();
            IList<WiFiNetwork> list = new List<WiFiNetwork>();
            list.Add(new WiFiNetwork("Datasets",50));
            netlist.Add(new WiFiNetworkSet(list));
            return netlist;
        }
        /// <summary>
        ///A test for WiFiConnectedSensor Constructor
        ///</summary>
        [TestMethod()]
        public void WiFiConnectedSensorConstructorTest()
        {
            double weigth = 5F; // TODO: Initialize to an appropriate value
            WiFiConnectedSensor target = new WiFiConnectedSensor(GetConnectedList(), weigth, null);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetDistance
        ///</summary>
        [TestMethod()]
        public void GetDistanceTest()
        {
            double weigth = 5.0;
            List<WiFiNetworkSet> connected2 = GetConnectedList();            
            WiFiConnectedSensor target = new WiFiConnectedSensor(GetConnectedList(), weigth,null);
            foreach (var wiFiNetwork in connected2.First().Networks)
            {
                wiFiNetwork.SignalQuality= wiFiNetwork.SignalQuality/2;
            }
            ISensor sensor = new WiFiConnectedSensor(connected2,weigth,null);
            double expected = 0.75F; 
            double actual;
            actual = target.GetDistance(sensor);
            Assert.AreEqual(expected, actual);
        }     

        [TestMethod()]
        public void GetDistanceDifferentSensorDataTest()
        {
            double weigth = 5.0;
            List<WiFiNetworkSet> connectedlist = GetConnectedList();
            List<WiFiNetwork> connected2 = new List<WiFiNetwork>();
            connected2.Add(new WiFiNetwork("b", 30));
            var conn2list = new List<WiFiNetworkSet>();
            conn2list.Add(new WiFiNetworkSet(connected2));
            WiFiConnectedSensor target = new WiFiConnectedSensor(connectedlist, weigth,null);
            WiFiConnectedSensor sensor = new WiFiConnectedSensor(conn2list,weigth,null);
            double expected = 0.0;
            var actual = target.GetDistance(sensor);
            Assert.AreEqual(expected,actual);
        }
    }
}
