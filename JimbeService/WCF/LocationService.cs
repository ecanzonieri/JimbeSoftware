using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoMapper;
using JimbeService.Business;
using JimbeService.IoC;
using CoreEntity = JimbeCore.Domain.Entities;
using DTO = JimbeWFC.DataContracts;
using JimbeWFC.ServiceContract;
using Location = JimbeWFC.DataContracts.Location;
using TracerX;

namespace JimbeService.WCF
{

    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.Single
  )]
    public class LocationService : ILocationService
    {

        private static Logger _logger = Logger.GetLogger("WCF Location Service");

        private ServiceManager _serviceManager;

        private IRepositoryFactory _repositoryFactory;

        private IMappingEngine _engine;

        public LocationService(IRepositoryFactory repositoryFactory, ServiceManager serviceManager, IMappingEngine engine)
        {
            _repositoryFactory = repositoryFactory;
            _serviceManager = serviceManager;
            _engine = engine;
        }

        #region Implementation of ILocationService

        public bool InsertLocation(DTO.Location location)
        {
            lock (_serviceManager)
            {
                var repository = _repositoryFactory.CreateRepository<Guid, CoreEntity.Location>();
                CoreEntity.Location corelocation = _engine.Map<DTO.Location, CoreEntity.Location>(location);
                corelocation = _serviceManager.PrepareLocation(corelocation);
                if (repository.Add(corelocation))
                {
                    _logger.Debug("Insert new location " + corelocation.Name + corelocation.Description);
                    return true;
                }
                _logger.Warn("Not possible to insert location: ");
                return false;
            }
        }

        public bool DeleteLocation(Location location)
        {
          
            var repository = _repositoryFactory.CreateRepository<Guid, CoreEntity.Location>();
            CoreEntity.Location corelocation = _engine.Map<DTO.Location, CoreEntity.Location>(location);
            var todelete = repository.FindBy(x => x.Name.Equals(corelocation.Name));
            if (ReferenceEquals(todelete, null)) return false;
            lock (_serviceManager)
            {
                if (repository.Delete(todelete))
                {
                    _logger.Debug("Delete location " + corelocation.Name + corelocation.Description);
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<DTO.Location> GetLocations()
        {
            var repository = _repositoryFactory.CreateRepository<Guid, CoreEntity.Location>();
            var locationlist = repository.All();
            var dotLocationList = _engine.Map<IEnumerable<CoreEntity.Location>, IEnumerable<DTO.Location>>(locationlist);
            _logger.Debug("Get location list");
            return dotLocationList;
        }

        public DTO.Location GetCurrentLocation()
        {
            lock (_serviceManager)
            {
                if (_serviceManager.Current!=null)
                    _logger.Debug("Current location "+ _serviceManager.Current.Name + _serviceManager.Current.Description);
                DTO.Location current = _engine.Map<CoreEntity.Location, DTO.Location>(_serviceManager.Current);
                return current;
            }
            
        }

        #endregion
    }
}
