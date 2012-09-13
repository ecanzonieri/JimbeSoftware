﻿using System;
using System.Collections.Generic;
using AutoMapper;
using JimbeService.Business;
using JimbeService.IoC;
using CoreEntity = JimbeCore.Domain.Entities;
using DTO = JimbeWFC.DataContracts;
using JimbeWFC.ServiceContract;
using Location = JimbeWFC.DataContracts.Location;

namespace JimbeService.WCF
{
    public class LocationService : ILocationService
    {
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

        public void InsertLocation(DTO.Location location)
        {
            var repository=_repositoryFactory.CreateRepository<Guid,CoreEntity.Location>();
            CoreEntity.Location corelocation = _engine.Map<DTO.Location, CoreEntity.Location>(location);
            corelocation = _serviceManager.PrepareLocation(corelocation);
            repository.Add(corelocation);
        }

        public void DeleteLocation(DTO.Location location)
        {
            var repository = _repositoryFactory.CreateRepository<Guid, CoreEntity.Location>();
            CoreEntity.Location corelocation = _engine.Map<DTO.Location, CoreEntity.Location>(location);
            var todelete = repository.FindBy(x => x.Name.Equals(corelocation.Name));
            if (ReferenceEquals(todelete,null)) return;
            repository.Delete(todelete);
        }

        public IEnumerable<DTO.Location> GetLocations()
        {
            var repository = _repositoryFactory.CreateRepository<Guid, CoreEntity.Location>();
            var locationlist = repository.All();
            var dotLocationList = _engine.Map<IEnumerable<CoreEntity.Location>, IEnumerable<DTO.Location>>(locationlist);
            return dotLocationList;
        }

        public Location GetCurrentLocation()
        {
            lock (_serviceManager)
            {
                return _engine.Map<CoreEntity.Location, DTO.Location>(_serviceManager.Current);    
            }
            
        }

        #endregion
    }
}