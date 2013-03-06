using System.Collections.Generic;
using System.ServiceModel;
using JimbeWCF.DataContracts;

namespace JimbeWCF.ServiceContract
{
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        bool InsertLocation(Location location);

        [OperationContract]
        bool DeleteLocation(Location location);

        [OperationContract]
        IEnumerable<Location> GetLocations();

        [OperationContract]
        Location GetCurrentLocation();
    }
}
