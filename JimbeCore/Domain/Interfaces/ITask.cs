using System.Collections.Generic;

namespace JimbeCore.Domain.Interfaces
{
    /// <summary>
    /// Task to be executed.
    /// </summary>
    public interface ITask
    {

        /// <summary>
        /// Start some task.
        /// </summary>
        /// <returns> True if task has been started successfully </returns>
        bool execute();

        /// <summary>
        /// Each task belongs to Location
        /// </summary>
        ILocation Location { get; set; }

        /// <summary>
        /// Statistics record the start and stop time of the task
        /// </summary>
        IList<IStatistic> Statistics { get; set; }
    }
}