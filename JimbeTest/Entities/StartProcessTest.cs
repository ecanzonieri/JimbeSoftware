using JimbeCore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Jimbe
{
    namespace JimbeTest
    {


        /// <summary>
        ///This is a test class for StartProcessTest and is intended
        ///to contain all StartProcessTest Unit Tests
        ///</summary>
        [TestClass()]
        public class StartProcessTest
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
            ///A test for StartProcess Constructor
            ///</summary>
            [TestMethod()]
            public void StartProcessConstructorTest()
            {
                string filename = "http://www.google.it"; // TODO: Initialize to an appropriate value
                string arguments = string.Empty; // TODO: Initialize to an appropriate value
                StartProcess target = new StartProcess(filename, arguments);
                Assert.IsNotNull(target);
            }

            /// <summary>
            ///A test for execute
            ///</summary>
            [TestMethod()]
            public void executeTest()
            {
                StartProcess target = new StartProcess("http:\\\\www.google.it", string.Empty);
                target.execute(null);
                Assert.IsTrue(target.Success);
            }
        }
    }
}