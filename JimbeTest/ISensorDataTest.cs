using Jimbe.JimbeCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JimbeTest
{
    
    
    /// <summary>
    ///This is a test class for ISensorDataTest and is intended
    ///to contain all ISensorDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ISensorDataTest
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


        internal virtual ISensorData CreateISensorData()
        {
            // TODO: Instantiate an appropriate concrete class.
            ISensorData target = null;
            return target;
        }

        /// <summary>
        ///A test for GetDistance
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void GetDistanceTest()
        {
            ISensorData target = CreateISensorData(); // TODO: Initialize to an appropriate value
            ISensorData sensorData = null; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = target.GetDistance(sensorData);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
