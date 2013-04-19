using System.Collections.Generic;
using System.ServiceModel;
using JimbeWCF.DataContracts;

namespace JimbeWCF.ServiceContract
{
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        InsertResult InsertLocation(Location location);

        [OperationContract]
        bool DeleteLocation(Location location);

        [OperationContract]
        IEnumerable<Location> GetLocations();

        [OperationContract]
        InsertResult UpdateLocation(Location oldlocation, Location newlocation);

        [OperationContract]
        Location GetCurrentLocation();
    }
}
