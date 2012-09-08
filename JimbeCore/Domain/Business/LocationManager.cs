using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Entities;
using JimbeCore.Domain.Interfaces;
using TracerX;

namespace JimbeCore.Domain.Business
{
    public class LocationManager 
    {
        private Location _current;

        private static Logger logger = Logger.GetLogger("LocationManager");

        private const double AffinityBound = 0.7F;

        private double _currentAffinity;

        public Location Current
        {
            get { return _current; }
        }

        public double CurrentAffinity
        {
            get { return _currentAffinity; }
        }

        #region ILocationManager Members

        public Location RecognizeLocation(Location location, IEnumerable<Location> locations )
        {
            double affinity = 0.0;
            Location winner = null;

            if (location == null)
            {
                logger.Error("location argument must not be null");
                throw new ArgumentNullException();
            }

            if (!locations.Any())
            {
                logger.Info("I haven't saved location");
                return null;
            }

            foreach (var location1 in locations)
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

        #endregion
    }
}