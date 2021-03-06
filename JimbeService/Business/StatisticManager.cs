﻿using System;
using JimbeCore.Domain.Entities;
using JimbeCore.Repository.Interfaces;
using JimbeService.IoC;

namespace JimbeService.Business
{
    public class StatisticManager
    {
        
        private IRepositoryFactory _repositoryFactory;
        
        public StatisticManager(IRepositoryFactory repositoryFactory, Location location)
        {
            _repositoryFactory = repositoryFactory;
            _current = new LocationStatistic()
                           {
                               Start = DateTime.Now,
                               Location = location
                           };
            lock (location)
                location.StatisticsList.Add(_current);

        }

        private LocationStatistic _current;

        public LocationStatistic Current { get { return _current; } }

        public void UpdateStatistic()
        {
            lock (_current.Location)
            {
                _current.End = DateTime.Now;
                IRepository<int, LocationStatistic> repository = _repositoryFactory.CreateRepository<int, LocationStatistic>();
                if (_current.Id.Equals(default(int)))
                {
                    repository.Add(_current);

                }
                else repository.Update(_current);
            }
        }

        public void UpdateStatistic(DateTime end)
        {
            lock (_current.Location)
            {
                _current.End = end;
                IRepository<int, LocationStatistic> repository = _repositoryFactory.CreateRepository<int, LocationStatistic>();
                if (_current.Id.Equals(default(int)))
                {
                    repository.Add(_current);

                }
                else repository.Update(_current);
            }
        }
    }
}
