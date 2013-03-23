using JimbeCore.Domain.Entities;

namespace JimbeCore.Exceptions
{
    public class LocationCollisionException : JimbeCoreException
    {
        public Location Conflict { get; set; }

        public LocationCollisionException()
        {
        }

        public LocationCollisionException(string message)
            : base(message)
        {
        }

        public LocationCollisionException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}