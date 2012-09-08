using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Jimbe.JimbeWiFi;
using JimbeCore.Domain.Entities;
using JimbeCore.Repository.Interfaces;
using JimbeService.IoC;
using TracerX;

namespace JimbeCore.Domain.Business
{
    public class ServiceManager
    {

        private TaskManager _taskManager;

        private static Logger logger = Logger.GetLogger("ServiceManager");

        private IRepositoryFactory<Guid, Location> _repositoryFactory;

        private WiFiManager _wifiManager;

        private LocationManager _locationManager;

        private StatisticManager _statisticManager;

        private Thread _taskThread;

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

        public ServiceManager(IRepositoryFactory<Guid,Location> repositoryFactory )
        {
            _repositoryFactory = repositoryFactory;
            _shouldstop = false;
            _wifiManager=new WiFiManager(JimbeWiFi.NativeApiVersion.windowsVista);
            _locationManager = new LocationManager();
        }

        public ServiceManager(WiFiManager wiFiManager, IRepositoryFactory<Guid, Location> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _wifiManager = wiFiManager;
            _locationManager = new LocationManager();
        }

        public void RunServiceManager()
        {
            while (!_shouldstop)
            {
                lock (Current)
                {
                    if (RecognizeLocation())
                    {
                        if (_taskManager != null)
                        {
                            // chiudere lo statisticManager
                            _taskManager.RequestStop();
                            _taskThread.Join();
                        }
                        if (!ReferenceEquals(_current, null))
                            _taskManager = new TaskManager(_current.Tasks);
                        _taskThread = new Thread(_taskManager.StartTasksExecution);
                    }

                    //Aggiornamento periodico StatisticManager
                    ///////////////////////
                }
                Thread.Sleep(10000);
            }
            if (_taskManager!=null)
            {
                // Chiudere lo statisticManager
                _taskManager.RequestStop();
                _taskThread.Join();
            }
        }

        private bool RecognizeLocation()
        {
            IRepository<Guid, Location> repository = _repositoryFactory.CreateRepository();

            Location unknown = new Location(getSensorsInfo());

            IList<Location> locations = repository.All().ToList();

            var result=_locationManager.RecognizeLocation(unknown, locations);

            if (result==null)
            {
                //Check affinity
                if (_locationManager.CurrentAffinity > 0.0)
                    logger.Info("Affinity too low: ", _locationManager.CurrentAffinity, " Current possible location ",
                                _locationManager.Current.Name);
                _current = null;
                return true;
            }
            if (!result.EqualsBusiness(_current))
            {
                _current = result;
                return true;
            }
            return false;

        }

        private IList<Sensor> getSensorsInfo()
        {
            IList<Sensor> sensors = new List<Sensor>();
            foreach (Sensor sensor in _wifiManager.GetAllWiFiSensorData())
                sensors.Add(sensor);
            foreach (Sensor sensor in _wifiManager.GetAllWiFiConnectedSensorData())
                sensors.Add(sensor);
            return sensors;
        }


        private void UpdateStatistic()
        {
            
        }

        public void RequestStop()
        {
            _shouldstop = true;
        }
    }
}
