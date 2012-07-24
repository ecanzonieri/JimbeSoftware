using Jimbe.JimbeCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Jimbe.JimbeWiFi;
using System.Collections.Generic;
using Ninject;
using Ninject.Parameters;

namespace JimbeTest
{


    /// <summary>
    ///This is a test class for WiFiSensorTest and is intended
    ///to contain all WiFiSensorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WiFiSensorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
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
        ///A test for WiFiSensor Constructor
        ///</summary>
        [TestMethod()]
        public void WiFiSensorConstructorTest()
        {
            IEnumerable<WifiNetwork> networks = getNetworks(0, 50, 5); // TODO: Initialize to an appropriate value
            double weigth = 1F; // TODO: Initialize to an appropriate value
            WiFiSensor target = new WiFiSensor(null,networks, weigth);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Networks
        ///</summary>
        [TestMethod()]
        public void NetworksTest()
        {
            IEnumerable<WifiNetwork> networks = getNetworks(0, 50, 5); // TODO: Initialize to an appropriate value
            double weigth = 0F; // TODO: Initialize to an appropriate value
            WiFiSensor target = new WiFiSensor(null,networks, weigth); // TODO: Initialize to an appropriate value
            IEnumerable<WifiNetwork> actual;
            actual = target.Networks;
            Assert.AreSame(networks, actual);
        }

        /// <summary>
        ///A test for Weigth
        ///</summary>
        [TestMethod()]
        public void WeigthTest()
        {
            IEnumerable<WifiNetwork> networks = null;
            double weigth = 5F;
            WiFiSensor target = new WiFiSensor(null,networks, weigth);
            double actual;
            actual = target.Weigth;
            Assert.AreEqual(weigth, actual);
        }

        /// <summary>
        ///A test for GetDistance
        ///</summary>
        [TestMethod]
        public void GetDistanceTest()
        {
            IEnumerable<WifiNetwork> networks = getNetworks(0, 100, 5);
            double weigth = 4F;
            WiFiSensor target = new WiFiSensor(null,networks, weigth);
            IEnumerable<WifiNetwork> networks2 = getNetworks(0, 50, 4);
            WiFiSensor target2 = new WiFiSensor(null,networks2, weigth);
            double actual = target.GetDistance(target2);
            double aspected = 0.75;
            Assert.AreEqual(aspected, actual);
        }

        /// <summary>
        /// Test GetDistance with same networks information.
        /// </summary>
        [TestMethod]
        public void GetDistanceTest1()
        {
            IEnumerable<WifiNetwork> networks = getNetworks(0, 50, 5);
            double weigth = 4F;
            WiFiSensor target = new WiFiSensor(null,networks, weigth);
            WiFiSensor target2 = new WiFiSensor(null,networks, weigth);
            double actual = target.GetDistance(target2);
            double aspected = 1F;
            Assert.AreEqual(aspected, actual);
        }

        /// <summary>
        /// Test distance with different networks.
        /// </summary>
        [TestMethod]
        public void GetDistanceTest2()
        {
            IEnumerable<WifiNetwork> networks = getNetworks(0, 100, 5);
            double weigth = 4F;
            WiFiSensor target = new WiFiSensor(null,networks, weigth);
            IEnumerable<WifiNetwork> networks2 = getNetworks(6, 50, 10);
            WiFiSensor target2 = new WiFiSensor(null,networks2, weigth);
            double actual = target.GetDistance(target2);
            double aspected = 0F;
            Assert.AreEqual(aspected, actual);
        }

        /// <summary>
        /// Test commutative property for GetDistance
        /// </summary>
        [TestMethod]
        public void GetDistanceTest3()
        {
            IEnumerable<WifiNetwork> networks = getNetworks(0, 60, 10);
            double weigth = 4F;
            WiFiSensor target = new WiFiSensor(null,networks, weigth);
            IEnumerable<WifiNetwork> networks2 = getNetworks(4, 30, 10);
            WiFiSensor target2 = new WiFiSensor(null,networks, weigth);
            double actual = target.GetDistance(target2);
            double aspected = target2.GetDistance(target);
            Assert.AreEqual(aspected, actual);
        }

    private LinkedList<WifiNetwork> getNetworks(int seed, int power, int nitems)
        {
            LinkedList<WifiNetwork> list = new LinkedList<WifiNetwork>();
            for (int i = seed; i < seed + nitems; i++)
                list.AddFirst(new WifiNetwork(i.ToString(), i.ToString(), null, WifiNetwork.BSSType.infrastructure, power));
            return list;
        }
    }
}
