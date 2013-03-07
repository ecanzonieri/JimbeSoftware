using System.Collections.Generic;

namespace JimbeCore.Domain.Interfaces
{
    /// <summary>
    /// Location is a set of "informations" which is identified with a name and description.
    /// </summary>
    /// <remarks>"Informations" are sensors given. Can exist differents type of sensors and different sensors of same type.</remarks>
    public interface ILocation
    {
        IEnumerable<ISensor> Sensors { get; }

        IEnumerable<IStatistic> Statistics { get; }

        IEnumerable<ITask> Tasks { get; } 

        string Name { get; set; }

        string Description { get; set; }

        double GetWeigthSum();

        /// <summary>
        /// Get the affinity with location
        /// </summary>
        /// <param name="location"> Is the other location that usually has to be compared with saved location, it can have only one dataset per sensor </param>
        /// <returns> double number that represents the affinity, 1 very similar 0 different </returns>
        double GetLocationAffinity(ILocation location);

        /// <summary>
        /// UpdateLocationSensors is used to update some informations of the sensors that identify the location. In this way different dataset can be used to have a smarter identification.
        /// </summary>
        /// <param name="location"> Is the location that contains the new dataset that has to be added </param>
        void UpdateLocationSensors(ILocation location);

        /// <summary>
        /// RemoveSensorsInfo is used to remove some informations of the sensors that identify the location.
        /// </summary>
        /// <param name="location"> Is the location that contains the new dataset that has to be removed </param>
        void RemoveSensorsInfo(ILocation location);
    }
}