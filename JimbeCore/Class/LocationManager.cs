using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jimbe
{
    namespace JimbeCore
    {
        public class LocationManager : ILocationManager
        {         
            private IEnumerable<ISensor> GetCommonSensors(ILocation location1, ILocation location2)
            {
                throw new NotImplementedException();
            }
        }
    }
}