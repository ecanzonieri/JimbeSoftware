using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jimbe
{
    namespace JimbeCore
    {
        public class Context : IContext
        {

            private string _name;
            private ILocation _location;
            private IEnumerable<ITask> _tasks;

            public Context()
            {
                _name = "default_contest_name";
                _location = null;
                _tasks = null;
            }

            public Context(string name, ILocation location, IEnumerable<ITask> tasks)
            {
                _name = name;
                _location = location;
                _tasks = tasks;
            }

            #region IContext Members

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public ILocation Location
            {
                get { return _location; }
                set { _location = value; }
            }

            public IEnumerable<ITask> Tasks
            {
                get { return _tasks; }
                set { _tasks = value; }
            }

            #endregion

        }
    }
}