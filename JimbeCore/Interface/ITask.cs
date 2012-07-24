using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jimbe
{
    namespace JimbeCore
    {
        /// <summary>
        /// Task to be executed.
        /// </summary>
        public interface ITask
        {
            /// <summary>
            /// Start some task.
            /// </summary>
            /// <returns> True if task has been started successfully </returns>
            bool execute();
        }
    }
}