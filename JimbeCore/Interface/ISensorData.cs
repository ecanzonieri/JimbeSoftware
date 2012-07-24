using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jimbe
{
    namespace JimbeCore
    {
        /// <summary>
        /// Sensor specific data
        /// </summary>
        /// <remarks>Measure given by sensor.</remarks>
        public interface ISensorData
        {
            /// <summary>
            /// Method that calculates the proximity between sensors specific information
            /// </summary>
            /// <returns>Value between 0-1 which represents the proximity</returns>
            double GetDistance(ISensorData sensorData);
        }
    }
}