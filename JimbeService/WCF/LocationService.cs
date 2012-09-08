using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using JimbeCore.Domain.Business;
using JimbeService.IoC;
using Ninject;
using CoreEntity = JimbeCore.Domain.Entities;
using DTO = JimbeWFC.DataContracts;
using JimbeCore.Repository.Interfaces;
using JimbeWFC.ServiceContract;
using NHibernate;
using Location = JimbeWFC.DataContracts.Location;

namespace JimbeService.WCF
{
    public class LocationService : ILocationService
    {
        private ServiceManager _serviceManager;

        private IRepositoryFactory<Guid, CoreEntity.Location> _repositoryFactory;

        private IMappingEngine _engine;

        public LocationService(IRepositoryFactory<Guid, CoreEntity.Location> repositoryFactory, ServiceManager serviceManager, IMappingEngine engine)
        {
            _repositoryFactory = repositoryFactory;
            _serviceManager = serviceManager;
            _engine = engine;
        }

        #region Implementation of ILocationService

        public void InsertLocation(DTO.Location location)
        {
            var repository=_repositoryFactory.CreateRepository();
            CoreEntity.Location corelocation = _engine.Map<DTO.Location, CoreEntity.Location>(location);
            repository.Add(corelocation);
        }

        public void DeleteLocation(DTO.Location location)
        {
            var repository = _repositoryFactory.CreateRepository();
            CoreEntity.Location corelocation = _engine.Map<DTO.Location, CoreEntity.Location>(location);
            var todelete = repository.FindBy(x => x.Name.Equals(corelocation.Name));
            if (ReferenceEquals(todelete,null)) return;
            repository.Delete(todelete);
        }

        public IEnumerable<DTO.Location> GetLocations()
        {
            var repository = _repositoryFactory.CreateRepository();
            var locationlist = repository.All();
            var dotLocationList = _engine.Map<IEnumerable<CoreEntity.Location>, IEnumerable<DTO.Location>>(locationlist);
            return dotLocationList;
        }

        public Location GetCurrentLocation()
        {
            lock (_serviceManager.Current)
            {
                return _engine.Map<CoreEntity.Location, DTO.Location>(_serviceManager.Current);    
            }
            
        }

        #endregion
    }
}
