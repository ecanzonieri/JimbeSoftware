using Jimbe.JimbeCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JimbeTest
{
    
    
    /// <summary>
    ///This is a test class for IContestTest and is intended
    ///to contain all IContestTest Unit Tests
    ///</summary>

    [TestClass()]
    public class IContestTest
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


        internal virtual IContext CreateIContest()
        {
            // TODO: Instantiate an appropriate concrete class.
            IContext target = null;
            return target;
        }

        /// <summary>
        ///A test for Location
        ///</summary>
        
        [TestMethod()]
        [Ignore]
        public void LocationTest()
        {
            IContext target = CreateIContest(); // TODO: Initialize to an appropriate value
            ILocation expected = null; // TODO: Initialize to an appropriate value
            ILocation actual;
            target.Location = expected;
            actual = target.Location;
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
            IContext target = CreateIContest(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Tasks
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void TasksTest()
        {
            IContext target = CreateIContest(); // TODO: Initialize to an appropriate value
            IEnumerable<ITask> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ITask> actual;
            target.Tasks = expected;
            actual = target.Tasks;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
