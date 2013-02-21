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
        void InsertLocation(Location location);

        [OperationContract]
        void DeleteLocation(Location location);

        [OperationContract]
        IEnumerable<Location> GetLocations();

        [OperationContract]
        Location GetCurrentLocation();
    }
}
