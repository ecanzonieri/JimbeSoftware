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
        void execute(object obj);

        /// <summary>
        /// Each task belongs to Location
        /// </summary>
        ILocation Location { get; set; }
    }
}