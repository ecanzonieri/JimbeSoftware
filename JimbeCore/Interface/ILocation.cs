using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Jimbe
{
    namespace JimbeCore
    {
        /// <summary>
        /// Location is a set of "informations" which is identified with a name and description.
        /// </summary>
        /// <remarks>"Informations" are sensors given. Can exist differents type of sensors and different sensors of same type.</remarks>
        public interface ILocation
        {
            IEnumerable<ISensor> Sensors { get; set; }

            string Name { get; set; }

            string Description { get; set; }


        }
    }
}