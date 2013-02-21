using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using JimbeWFC.DataContracts;

namespace JimbeWFC.ServiceContract
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
