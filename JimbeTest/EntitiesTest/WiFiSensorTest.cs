using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JimbeTest.EntitiesTest
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
            var networks = getNetworks(0, 50, 5); // TODO: Initialize to an appropriate value
            double weigth = 1F; // TODO: Initialize to an appropriate value
            WiFiSensor target = new WiFiSensor(networks, weigth, null);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for EqualsBusiness
        ///</summary>
        [TestMethod()]
        public void EqualsBusinessTest()
        {
            var networks = getNetworks(0, 50, 5); // TODO: Initialize to an appropriate value
            double weigth = 0F; // TODO: Initialize to an appropriate value
            WiFiSensor assumption = new WiFiSensor(networks, weigth,null); // TODO: Initialize to an appropriate value
            WiFiSensor target = new WiFiSensor(networks, weigth, null);
            Assert.IsTrue(assumption.EqualsBusiness(target));
        }
        [TestMethod()]
        public void EqualsBusinessTest2()
        {
            double weigth = 0F; // TODO: Initialize to an appropriate value
            WiFiSensor assumption = new WiFiSensor(getNetworks(0, 50, 5), weigth, null); // TODO: Initialize to an appropriate value
            WiFiSensor target = new WiFiSensor(getNetworks(2, 50, 5), weigth, null);
            Assert.IsFalse(assumption.EqualsBusiness(target));
        }

        ///<summary>
        /// A test for EqualBusiness different Datasets
        /// </summary>

        /// <summary>
        ///A test for Weigth
        ///</summary>
        [TestMethod()]
        public void WeigthTest()
        {
            double weigth = 5F;
            WiFiSensor target = new WiFiSensor(null, weigth, null);
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
            var networks = getNetworks(0, 100, 5);
            double weigth = 4F;
            WiFiSensor target = new WiFiSensor(networks, weigth, null);
            var networks2 = getNetworks(0, 50, 4);
            WiFiSensor target2 = new WiFiSensor(networks2, weigth, null);
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
            var networks = getNetworks(0, 50, 5);
            double weigth = 4F;
            WiFiSensor target = new WiFiSensor(networks, weigth, null);
            WiFiSensor target2 = new WiFiSensor(networks, weigth, null);
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
            var networks = getNetworks(0, 100, 5);
            double weigth = 4F;
            WiFiSensor target = new WiFiSensor(networks, weigth, null);
            var networks2 = getNetworks(6, 50, 10);
            WiFiSensor target2 = new WiFiSensor(networks2, weigth, null);
            double actual = target.GetDistance(target2);
            double aspected = 0F;
            Assert.AreEqual(aspected, actual);
        }

        /// <summary>
        /// Test Update sensor
        /// </summary>
        [TestMethod]
        public void UpdateSensorDatasetTest()
        {
            var networks = getNetworks(0, 60, 10);
            double weigth = 4F;
            int aspected = networks.Count;
            WiFiSensor target = new WiFiSensor(networks, weigth, null);
            var networks2 = getNetworks(5, 80, 10);
            WiFiSensor second = new WiFiSensor(networks2, weigth, null);
            target.UpdateSensorDataset(second);
            int targetcount = target.Datasets.Count;
            aspected += networks2.Count;
            Assert.AreEqual(aspected, targetcount);
        }

    private List<WiFiNetworkSet> getNetworks(int seed, int power, int nitems)
        {
            var list = new List<WiFiNetworkSet>();
            var netlist = new List<WiFiNetwork>();
            for (int i = seed; i < seed + nitems; i++)
                netlist.Add(new WiFiNetwork(i.ToString() , power));
            list.Add(new WiFiNetworkSet(netlist));
            netlist=new List<WiFiNetwork>();
            for (int i = seed+2; i < seed + nitems; i++)
                netlist.Add(new WiFiNetwork(i.ToString(), power));
            list.Add(new WiFiNetworkSet(netlist));
            return list;
        }
    }
}
