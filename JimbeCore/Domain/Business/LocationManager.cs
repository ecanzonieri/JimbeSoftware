using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using TracerX;

namespace JimbeCore.Domain.Entities
{
    public class LocationManager : ILocationManager
    {
        private ILocation _current;
        private LinkedList<ILocation> _locations;

        private static Logger logger = Logger.GetLogger("LocationManager");

        private const double AffinityBound = 0.7F;

        private double _currentAffinity;

        public LocationManager(LinkedList<ILocation> locations)
        {
            _locations = locations;
            Logger.DefaultBinaryFile.Open();
        }

        public ILocation Current
        {
            get { return _current; }
        }

        public double CurrentAffinity
        {
            get { return _currentAffinity; }
        }

        public IEnumerable<ILocation> Locations
        {
            get { return _locations; }
        }

        #region ILocationManager Members

        public ILocation RecognizeLocation(ILocation location)
        {
            double affinity = 0.0;
            ILocation winner = null;

            if (location == null)
            {
                logger.Error("location argument must not be null");
                throw new ArgumentNullException();
            }

            if (!Locations.Any())
            {
                logger.Info("I haven't saved location");
                return null;
            }

            foreach (var location1 in Locations)
            {
                double affinityTmp;
                if ((affinityTmp = location1.GetLocationAffinity(location)) > affinity)
                {
                    affinity = affinityTmp;
                    winner = location1;
                }
            }
            if (winner == null)
            {
                logger.Info("Information is not present for this set of sensors");
                return null;
            }
            if (affinity < AffinityBound)
            {
                logger.Info("Location identified as {0} but affinity is under the bound. Affinity value is {1}",
                            winner.Name, affinity);
                _current = winner;
                _currentAffinity = affinity;
                return null;
            }

            logger.Info("Location identified as {0} with affinity {1}", winner.Name, affinity);
            _current = winner;
            _currentAffinity = affinity;
            return winner;
        }

        public void AddLocation(ILocation location)
        {
            _locations.AddFirst(location);
        }

        public bool DeleteLocation(ILocation location)
        {
            return _locations.Remove(location);
        }

        #endregion
    }
}