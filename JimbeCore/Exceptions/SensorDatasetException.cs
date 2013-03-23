namespace JimbeCore.Exceptions
{
    public class SensorDatasetException : JimbeCoreException
    {
         public SensorDatasetException()
        {
        }

        public SensorDatasetException(string message)
            : base(message)
        {
        }

        public SensorDatasetException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}