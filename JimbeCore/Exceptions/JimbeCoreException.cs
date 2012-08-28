namespace JimbeCore.Exceptions
{
    public class JimbeCoreException : System.Exception
    {
        public JimbeCoreException()
        {
        }

        public JimbeCoreException(string message)
            : base(message)
        {
        }

        public JimbeCoreException(string message, System.Exception inner)
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

        public SensorDifferentException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}