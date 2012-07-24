using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jimbe
{
    namespace JimbeCore
    {
        /// <summary>
        /// ISensor is a information element.
        /// </summary>
        /// <remarks>Core of ISensor is ISensorData that is measure given by sensor.</remarks>
        public interface ISensor
        {
            /// <summary>
            /// Sensor Weigth can be used in weighted average calculated between different kind of sensor
            /// </summary>
            double Weigth { get; }
            
            /// <summary>
            /// Method that calculates the proximity between sensors specific information
            /// </summary>
            /// <returns>Value between 0-1 which represents the proximity</returns>
            /// <exception cref="JimbeCoreException">If sensor type is not compatible getDistance can throws an Exception</exception>
            double GetDistance(ISensor sensor);

            /// <summary>
            /// Compare two sensors
            /// </summary>
            /// <remarks>If sensors are of different type return false else it checks on specific sensor identifier.</remarks>
            /// <param name="sensor"></param>
            /// <returns>True if sensor are equal false otherwise</returns>
            bool Equals(ISensor sensor);

        }
    }
}