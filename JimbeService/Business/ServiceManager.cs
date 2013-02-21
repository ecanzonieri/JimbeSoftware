using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Jimbe.JimbeWiFi;
using JimbeCore.Domain.Entities;
using JimbeCore.Repository.Interfaces;
using JimbeService.IoC;
using TracerX;

namespace JimbeService.Business
{
    public class ServiceManager
    {

        private TaskManager _taskManager;

        private static Logger logger = Logger.GetLogger("ServiceManager");

        private IRepositoryFactory _repositoryFactory;

        private WiFiManager _wifiManager;

        private LocationManager _locationManager;

        private StatisticManager _statisticManager;

        private volatile bool _shouldstop;

        private Location _current;

        public Location Current
        {
            get { return _current; }
        }

        public LocationManager LocationProvider
        {
            get { return _locationManager; }
        }

        public ServiceManager(IRepositoryFactory repositoryFactory )
        {
            _repositoryFactory = repositoryFactory;
            _shouldstop = false;
            _wifiManager=new WiFiManager(JimbeWiFi.NativeApiVersion.windowsVista);
            _locationManager = new LocationManager();
        }

        public ServiceManager(WiFiManager wiFiManager, IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _wifiManager = wiFiManager;
            _locationManager = new LocationManager();
        }

        public void RunServiceManager()
        {
            logger.Debug("ServiceManager Started");
            while (!_shouldstop)
            {
                lock (this)
                {
                    if (RecognizeLocation())
                    {
                        logger.Info("RecognizeLocation gave me a new location");
                        if (_taskManager != null)
                            _taskManager.RequestStop();
                        _statisticManager = new StatisticManager(_repositoryFactory, _current);
                        _taskManager = new TaskManager(_current.Tasks);
                        _taskManager.StartTasksExecution();
                    }
                    if (_statisticManager != null) _statisticManager.UpdateStatistic();

                }
                Thread.Sleep(10000);
            }
            
            if (_taskManager!=null && _statisticManager!=null)
            {
                lock (this)
                {
                    _statisticManager.UpdateStatistic();
                    _taskManager.RequestStop();
                }
            }
        }

        private bool RecognizeLocation()
        {
            IRepository<Guid, Location> repository = _repositoryFactory.CreateRepository<Guid,Location>();

            var unknown = new Location();
            unknown.SensorsList = GetSensorsInfo(unknown);

            IList<Location> locations = repository.All().ToList();

            var result=_locationManager.RecognizeLocation(unknown, locations);

            if (ReferenceEquals(result,null))
            {
                if (_taskManager!=null) _taskManager.RequestStop();
                _statisticManager = null;
                _current = null;
                

                //Debug Information check affinity
                if (_locationManager.CurrentAffinity > 0.0)
                    logger.Info("Affinity too low: ", _locationManager.CurrentAffinity, " Current possible location ",
                                _locationManager.Current.Name);
                return false;
            }
            if (!result.EqualsBusiness(_current))
            {
                logger.Info(" Location Name: ",
                                _locationManager.Current.Name, "Location Affinity : ", _locationManager.CurrentAffinity);
                _current = result;
                return true;
            }
            return false;

        }

        private IList<Sensor> GetSensorsInfo(Location location)
        {
            logger.Debug("Give me current sensors informations");
            IList<Sensor> sensors = new List<Sensor>();
            foreach (Sensor sensor in _wifiManager.GetAllWiFiSensorData())
            {
                sensor.Location = location;
                sensors.Add(sensor);
            }
            foreach (Sensor sensor in _wifiManager.GetAllWiFiConnectedSensorData())
            {
                sensor.Location = location;
                sensors.Add(sensor);   
            }
            return sensors;
        }

        public Location PrepareLocation(Location location)
        {
            location.SensorsList = GetSensorsInfo(location);
            foreach (var task in location.TasksList)
            {
                task.Location = location;
            }
            return location; 
        }

        public void RequestStop()
        {
            _shouldstop = true;
        }
    }
}
