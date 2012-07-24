using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jimbe
{
    namespace JimbeCore
    {
        /// <summary>
        /// IContext represents operative context in which some tasks are executed.
        /// </summary>
        public interface IContext
        {
            string Name { get; set; }

            ILocation Location { get; set; }

            IEnumerable<ITask> Tasks { get; set; }
        }
    }
}