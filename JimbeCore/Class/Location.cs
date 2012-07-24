using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Jimbe
{
    namespace JimbeCore
    {
        public class Location : ILocation
        {
            private IEnumerable<ISensor> _sensors;
            private string _name, _description;

            public Location (string name, string description, IEnumerable<ISensor> sensors )
            {
                _sensors = sensors;
                _description = description;
                _name = name;
            }

            public Location (string name, IEnumerable<ISensor> sensors )
            {
                _sensors = sensors;
                _name = name;
                _description = "No description available for this location";
            }


            #region ILocation members

            public IEnumerable<ISensor> Sensors
            {
                get { return _sensors; }
                set { _sensors = value; }
            }

            public string Name
            {
                get { return _name; }
                set
                {
                    if (value != null) _name = value;
                    else throw new ArgumentException("Name must be not null");
                }
            }

            public string Description
            {
                get { return _description; }
                set
                {
                    if (value!=null) _description = value;
                    else throw  new ArgumentException("Description must be not null");
                }
            }

            #endregion
        }
    }
}