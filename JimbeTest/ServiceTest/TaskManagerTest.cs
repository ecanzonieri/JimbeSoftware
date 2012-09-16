using System;
using System.Collections.Generic;
using System.Threading;
using JimbeCore.Domain.Entities;
using JimbeService.Business;
using JimbeTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JimbeTest.ServiceTest
{
    /// <summary>
    /// Summary description for TaskManagerTest
    /// </summary>
    [TestClass]
    public class TaskManagerTest
    {
        public TaskManagerTest()
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
        public void StartTasksExecutionTest()
        {
            bool succ=true;
            var tasks = TaskHelper.GetTasks();
            TaskManager taskManager= new TaskManager(tasks);
            taskManager.StartTasksExecution();
            Thread.Sleep(new TimeSpan(0, 0, 0, 70));
            foreach (var task in tasks)
            {
                if (!task.Success && task.Type!=Task.TaskType.Delayed) 
                    succ = false;
            }

            Assert.IsTrue(succ);

        }
    }
}
