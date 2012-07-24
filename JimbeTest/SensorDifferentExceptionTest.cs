using Jimbe.JimbeCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JimbeTest
{
    
    
    /// <summary>
    ///This is a test class for SensorDifferentExceptionTest and is intended
    ///to contain all SensorDifferentExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SensorDifferentExceptionTest
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
        ///A test for SensorDifferentException Constructor
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void SensorDifferentExceptionConstructorTest()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            Exception inner = null; // TODO: Initialize to an appropriate value
            SensorDifferentException target = new SensorDifferentException(message, inner);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SensorDifferentException Constructor
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void SensorDifferentExceptionConstructorTest1()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            SensorDifferentException target = new SensorDifferentException(message);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SensorDifferentException Constructor
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void SensorDifferentExceptionConstructorTest2()
        {
            SensorDifferentException target = new SensorDifferentException();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
