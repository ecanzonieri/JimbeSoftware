using Jimbe.JimbeCore;
using Jimbe.JimbeWiFi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ninject.Parameters;

namespace Jimbe
{

    namespace JimbeTest
    {

        /// <summary>
        ///This is a test class for ISensorTest and is intended
        ///to contain all ISensorTest Unit Tests
        ///</summary>
        [TestClass()]
        public class ISensorTest
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


            internal virtual ISensor CreateISensor()
            {
                // TODO: Instantiate an appropriate concrete class.
                ISensor target =
                    new WiFiConnectedSensor(null,
                        new WifiNetwork("connected", "", null, WifiNetwork.BSSType.infrastructure, 50), 5);
                return target;
            }

            /// <summary>
            ///A test for GetDistance
            ///</summary>
            [TestMethod()]
            public void GetDistanceTest()
            {
                ISensor target = CreateISensor(); // TODO: Initialize to an appropriate value
                ISensor sensor = null; // TODO: Initialize to an appropriate value
                double expected = 0F; // TODO: Initialize to an appropriate value
                double actual;
                actual = target.GetDistance(sensor);
                Assert.AreEqual(expected, actual);
                Assert.Inconclusive("Verify the correctness of this test method.");
            }

            /// <summary>
            ///A test for Weigth
            ///</summary>
            [TestMethod()]
            public void WeigthTest()
            {
                ISensor target = CreateISensor(); // TODO: Initialize to an appropriate value
                double actual;
                actual = target.Weigth;
                Assert.Inconclusive("Verify the correctness of this test method.");
            }

            [TestMethod]
            [Ignore]
            [ExpectedException(typeof(SensorDifferentException))]
            public void TestDifferentKindOfSensor()
            {
            }
        }
    }
}