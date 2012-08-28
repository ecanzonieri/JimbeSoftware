using JimbeCore.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JimbeTest
{
    
    
    /// <summary>
    ///This is a test class for JimbeCoreExceptionTest and is intended
    ///to contain all JimbeCoreExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JimbeCoreExceptionTest
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
        ///A test for JimbeCoreException Constructor
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void JimbeCoreExceptionConstructorTest()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            Exception inner = null; // TODO: Initialize to an appropriate value
            JimbeCoreException target = new JimbeCoreException(message, inner);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for JimbeCoreException Constructor
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void JimbeCoreExceptionConstructorTest1()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            JimbeCoreException target = new JimbeCoreException(message);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for JimbeCoreException Constructor
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void JimbeCoreExceptionConstructorTest2()
        {
            JimbeCoreException target = new JimbeCoreException();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
