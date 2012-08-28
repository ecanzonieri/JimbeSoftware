using System.Collections.Generic;

namespace JimbeCore.Domain.Interfaces
{
    public interface ILocationManager
    {

        ILocation Current { get; }

        IEnumerable<ILocation> Locations { get; }

        ILocation RecognizeLocation(ILocation location);

        void AddLocation(ILocation location);

        bool DeleteLocation(ILocation location);

    }
}