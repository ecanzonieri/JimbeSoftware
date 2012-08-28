﻿using JimbeCore.Exceptions;

namespace JimbeCore.Domain.Interfaces
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
        double Weigth { get; set; }

        /// <summary>
        /// ISensor belongs to Location.
        /// </summary>
        ILocation Location { get; set; }

        /// <summary>
        /// Method that calculates the proximity between sensors specific information
        /// </summary>
        /// <returns>Value between 0-1 which represents the proximity</returns>
        /// <exception cref="JimbeCoreException">If sensor type is not compatible getDistance can throws an Exception</exception>
        double GetDistance(ISensor sensor);

    }
}