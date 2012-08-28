using JimbeCore.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JimbeTest
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


        internal virtual ILocation CreateILocation()
        {
            // TODO: Instantiate an appropriate concrete class.
            ILocation target = null;
            return target;
        }

        /// <summary>
        ///A test for Description
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void DescriptionTest()
        {
            ILocation target = CreateILocation(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Description = expected;
            actual = target.Description;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void NameTest()
        {
            ILocation target = CreateILocation(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Sensors
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void SensorsTest()
        {
            ILocation target = CreateILocation(); // TODO: Initialize to an appropriate value
            IList<ISensor> expected = null; // TODO: Initialize to an appropriate value
            IList<ISensor> actual;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
