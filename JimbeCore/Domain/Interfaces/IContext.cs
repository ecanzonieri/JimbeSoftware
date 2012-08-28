using System.Collections.Generic;

namespace JimbeCore.Domain.Interfaces
{
    /// <summary>
    /// IContext represents operative context in which some tasks are executed.
    /// </summary>
    public interface IContext
    {
        string Name { get; set; }

        /// <summary>
        /// There's a one to one relashionship with Location 
        /// </summary>
        ILocation Location { get; set; }

        IList<ITask> Tasks { get; set; }

        IList<IStatistic> Statistics { get; set; }
    }
}