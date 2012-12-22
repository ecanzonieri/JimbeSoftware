using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeApp.Helpers;
using JimbeWFC.DataContracts;
using JimbeWFC.ServiceContract;

namespace JimbeApp.Models
{
  public  class WPF_Location : NotificationObject
    {
        private Location _Location;       
        public WPF_Location(Location loc)
        {
            if(loc!= null)
            _Location = loc;
        }

      public WPF_Location()
      {
          _Location=new Location();
          Name = "Nessuna locazione trovata";
          Description = "locazione non valida";
          TasksList = null;
          SensorsList = null;
          StatisticList = null;

      }
        # region proprieties

        public string Name
        {
            get { return _Location.Name; }
            set
            { 
                if (this._Location.Name != value)
                {
                    this._Location.Name = value;
                    RaisePropertyChanged( () => Name);
                }
            
            }
        }

        public string Description
        {
            get { return _Location.Description; }
            set
            {
                if (this._Location.Description != value)
                {
                    this._Location.Description = value;
                    RaisePropertyChanged(() => Description);
                }

            }
        }

        public IList<Statistic> StatisticList
        {
            get { return _Location.StatisticsList; }
            set
            {
                if (this._Location.StatisticsList != value)
                {
                    this._Location.StatisticsList = value;
                    RaisePropertyChanged(() => StatisticList);
                }

            }
        }

        public  IList<Sensor> SensorsList 
        {
            get { return _Location.SensorsList; }
            set
            {
                if (this._Location.SensorsList != value)
                {
                    this._Location.SensorsList = value;
                    RaisePropertyChanged(() => SensorsList);
                }

            }
        }

        public IList<Task> TasksList
        {
            get { return _Location.TasksList; }
            set
            {
                if (this._Location.TasksList != value)
                {
                    this._Location.TasksList = value;
                    RaisePropertyChanged(() => TasksList);
                }

            }
        }

      private int _countTask;
        public int CountTasks
        {
            get { return this._countTask; }
            set
            {
                if (this._countTask != value)
                {
                    this._countTask = value;
                    RaisePropertyChanged(() => CountTasks);
                }

            }
        }


        #endregion


        #region methods      
        #endregion
    }
}
