using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jimbe
{
    namespace JimbeCore
    {
        public class JimbeCoreException : Exception
        {
            public JimbeCoreException()
            {
            }

            public JimbeCoreException(string message)
                : base(message)
            {
            }

            public JimbeCoreException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class SensorDifferentException : JimbeCoreException
        {
            public SensorDifferentException()
            {
            }

            public SensorDifferentException(string message)
                : base(message)
            {
            }

            public SensorDifferentException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}