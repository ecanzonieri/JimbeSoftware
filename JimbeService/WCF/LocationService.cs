using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using AutoMapper;
using JimbeCore.Exceptions;
using JimbeService.Business;
using JimbeService.IoC;
using JimbeWCF.ServiceContract;
using CoreEntity = JimbeCore.Domain.Entities;
using DTO = JimbeWCF.DataContracts;
using Location = JimbeWCF.DataContracts.Location;
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

        public DTO.InsertResult InsertLocation(DTO.Location location)
        {
            lock (_serviceManager)
            {
                var repository = _repositoryFactory.CreateRepository<Guid, CoreEntity.Location>();
                CoreEntity.Location corelocation = _engine.Map<DTO.Location, CoreEntity.Location>(location);
                try
                {
                    corelocation = _serviceManager.PrepareLocation(corelocation);
                }
                catch (LocationCollisionException lce)
                {
                    return new DTO.InsertResult()
                        {
                            Conflict = _engine.Map<CoreEntity.Location, DTO.Location>(lce.Conflict),
                            Result = false
                        };
                }
                if (repository.Add(corelocation))
                {
                    _logger.Debug("Insert new location " + corelocation.Name + corelocation.Description);
                    return new DTO.InsertResult() {Result = true};
                }
                _logger.Warn("Not possible to insert location: ");
                return new DTO.InsertResult() { Result = false }; 
            }
        }

        public DTO.InsertResult UpdateLocation(DTO.Location oldlocation, DTO.Location newlocation)
        {
            lock (_serviceManager)
            {
                var repository = _repositoryFactory.CreateRepository<Guid, CoreEntity.Location>();
                var oldcorelocation = repository.All().FirstOrDefault(x => x.Name==oldlocation.Name);

                CoreEntity.Location newcorelocation = _engine.Map<DTO.Location, CoreEntity.Location>(newlocation);

                if (oldcorelocation.Name != newcorelocation.Name) 
                    oldcorelocation.Name = newcorelocation.Name;
                if (oldcorelocation.Description != newcorelocation.Description) 
                    oldcorelocation.Description = newcorelocation.Description;
                oldcorelocation.TasksList = newcorelocation.TasksList;
                foreach (CoreEntity.Task task in oldcorelocation.TasksList) task.Location = oldcorelocation;

                if (repository.Update(oldcorelocation))
                {
                    _logger.Debug("Updated location " + oldcorelocation.Name + " " + oldcorelocation.Description + " in " + newcorelocation.Name + " " + oldcorelocation.Description);
                    return new DTO.InsertResult() { Result = true };
                }
                _logger.Warn("Not possible to update location: ");
                return new DTO.InsertResult() { Result = false };
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
