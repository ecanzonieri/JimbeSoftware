using System;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JimbeTest.EntitiesTest
{
    /// <summary>
    /// Summary description for LocationStatisticTest
    /// </summary>
    [TestClass]
    public class StatisticTest
    {
        public StatisticTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetPeriodTest()
        {
            IStatistic target = new LocationStatistic()
                                       {
                                           Start=new DateTime(2012,9,14,0,14,0),
                                           End = new DateTime(2012,9,14,1,30,0)
                                       };
            TimeSpan expected=new TimeSpan(0,1,16,0);
            var actual=target.GetPeriod();
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void GetIsFinished()
        {
            IStatistic target = new LocationStatistic()
            {
                Start = new DateTime(2012, 9, 14, 0, 14, 0),
            };
            Assert.IsFalse(target.IsFinished());
        }
    }
}
