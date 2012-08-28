using System;

namespace JimbeCore.Domain.Interfaces
{
    public interface IStatistic
    {

        DateTime Start { get; set; }

        DateTime End { get; set; }

        TimeSpan GetLeast();

        bool IsFinished();
    }
}
