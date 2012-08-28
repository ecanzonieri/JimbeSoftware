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

        IList<IStatistic> Statistics { get; set; }

        IEnumerable<ITask> Tasks { get; } 

        string Name { get; set; }

        string Description { get; set; }

        double GetWeigthSum();

        double GetLocationAffinity(ILocation location);
    }
}