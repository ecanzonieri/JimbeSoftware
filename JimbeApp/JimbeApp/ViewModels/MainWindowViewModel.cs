using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using JimbeApp.Helpers;
using JimbeWCF.DataContracts;
using JimbeWCF.ServiceContract;
using Jimbe.JimbeWiFi;



//per far partire il gestore servizio usare: wcftestclient dal prompt di visual studio2010
namespace JimbeApp.ViewModels
{

    public enum TaskType
    {
        Spot,
        Periodic,
        Delayed
    }
	
	public enum Sens
	{
	    Alta =3,
        Media=2,
        Bassa=1,    
	}


    public class MainWindowViewModel : BaseViewModel
    {
        private DispatcherTimer dispatcherTimer;
        private ILocationService _proxy;
        private ServiceController jimbeService=null;


        #region Properties

        private int TaskIndex = -1;
        private int _countTasks;
        public int CountTasks
        {
            get { return _countTasks; }
            set
            {
                if (this._countTasks != value)
                {
                    this._countTasks = value;
                    RaisePropertyChanged(() => CountTasks);
                }
            }
        }

       #region tasks data         
        private String _Filename;
        public String Filename
        {
            get { return _Filename; }
            set
            {
                if (this._Filename != value)
                {
                    this._Filename = value;
                    RaisePropertyChanged(() => Filename);
                }   
            }
        }

        private String _url;
        public String Url
        {
            get { return _url; }
            set
            {
                if (this._url != value)
                {
                    this._url = value;
                    RaisePropertyChanged(() => Url);
                }
            }
        }
        private Task.TaskType _Type;
       public Task.TaskType Type { 
            get { return _Type; }
            set
            {
                if (this._Type != value)
                {
                    this._Type = value;
                    RaisePropertyChanged(() => Type);
                }      
            }
        }

        private TimeSpan _Delay;
        TimeSpan Delay
        {
            get { return _Delay; } 
            set
            {
                if (this._Delay != value)
                {
                    this._Delay = value;
                    RaisePropertyChanged(() => Delay);
                }  
            }
        }

        private int _seconds;
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                if (this._seconds != value)
                {
                    this._seconds = value;
                    RaisePropertyChanged(() => Seconds);
                }
            }
        }
        private int _minutes;
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (this._minutes != value)
                {
                    this._minutes = value;
                    RaisePropertyChanged(() => Minutes);
                }
            }
        }
        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set
            {
                if (this._hours != value)
                {
                    this._hours = value;
                    RaisePropertyChanged(() => Hours);
                }
            }
        }
               
        private ObservableCollection<StartProcess> _TasksList;
        public ObservableCollection<StartProcess> TasksList
        {
            get { return _TasksList; }
            set
            {
                if (this._TasksList != value)
                {
                    this._TasksList = value;
                    RaisePropertyChanged(() => TasksList);
                }   
            }
        }

          #endregion

       #region Locations data

        private Location tmpupLocation;
        private string _nameLocationStr;
        public string NameLocationStr
        {
            get { return _nameLocationStr; }
            set
            {
                if (this._nameLocationStr != value)
                {
                    this._nameLocationStr = value;
                    RaisePropertyChanged(() => NameLocationStr);
                }
            }
        }

        private string _descriptionLocationStr;
        public string DescriptionLocationStr
        {
            get { return _descriptionLocationStr; } 
            set
            {
                if (this._descriptionLocationStr != value)
                {
                    this._descriptionLocationStr = value;
                    RaisePropertyChanged(() => DescriptionLocationStr);
                }
            }
        }
        public IList<Sensor> LocationSensorList;
        public IList<Statistic> LocationStatisticList; 
        private ObservableCollection<Location> _locationsList;
        public ObservableCollection<Location> LocationsList
        {
            get 
            {              
                return _locationsList;                
            }
            set
            {
                if (this._locationsList != value)
                {
                    this._locationsList = value;
                    RaisePropertyChanged(() => LocationsList);
                }
            }
        }

                  private Location _Llocation;
          public Location Llocation 
        {
            get { return _Llocation; }
            set
            {
                if (this._Llocation != value)
                {
                    this._Llocation = value;
                    RaisePropertyChanged(() => Llocation);
                }
            }
        }

        #endregion

        #region statistic Data

        private ObservableCollection<KeyValuePair<string, Double>> _statisticList;
        public ObservableCollection<KeyValuePair<string, Double>> StatisticList
        {
            get { return _statisticList; }
            set
            {
                 if(value != this._statisticList)         
                 {
                this._statisticList = value;
                RaisePropertyChanged(() => StatisticList);
                 }
            }
        }

        #endregion

        #region settingData

        private bool _checkedconnectedsaved;
        private bool _checkedconnected;

        public bool Checkedconnected
        {
            get { return _checkedconnected; }
            set
            {
                if (this._checkedconnected != value)
                {
                    this._checkedconnected = value;
                    RaisePropertyChanged(() => Checkedconnected);
                }
            }
        }

        private int _sensibilitàSaved;
        private int _sensibilità;
        public int Sensibilità
        {
             get { return _sensibilità; }
            set
            {
                if (this._sensibilità != value)
                {
                    this._sensibilità = value;
                    RaisePropertyChanged(() => Sensibilità);
                }
            }
        }

        private List<String> _interfacesSaved;
        private List<String> _interfaces;
        public List<String> Interfaces
        {
            get { return _interfaces; }
            set
            {
                if (this._interfaces != value)
                {
                    this._interfaces = value;
                    RaisePropertyChanged(() => Interfaces);
                }
            }
        }

        #endregion

        #endregion

        #region Commands
        public ICommand GetStatus { get { return new DelegateCommand(GetStatus_f); } }
        public ICommand StoreLocation { get { return new DelegateCommand(StoreLocation_f); } }
        public ICommand ManagePrefered { get { return new DelegateCommand(ManagePrefered_f); } }
        public ICommand ShowStats{get {return new DelegateCommand(ShowStats_f);}}        
        public ICommand AddTask { get { return new DelegateCommand(AddTask_f); } }
        public ICommand NewTask { get { return new DelegateCommand(NewTask_f); } }
        public ICommand AddUrl { get { return new DelegateCommand(AddUrl_f); } }
        public ICommand NewUrl { get { return new DelegateCommand(NewUrl_f); } }
		public ICommand Setting { get { return new DelegateCommand(Setting_f); } }
        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            //mettere un blocco try catch e modificare i metodi in modo che se proxy è null invalida il menu 

       /*       try
          {
                jimbeService = new ServiceController("JimbeService");

            }
            catch (Exception e)
            {
                MessageBox.Show("Servizio non presente. Assicurarsi di averlo installato");
               Application.Current.Shutdown();

            }

            if (jimbeService.Status == ServiceControllerStatus.Running)
            {
                //servizio attivo
                //mi collego al servizio tramite wcf per ottenere la locazione attuale

                try
                { */
                    _proxy = ProxyFactory.GetProxy(Properties.Settings.Default.Url_Client);
            /*
                }
                catch (Exception)
                {
                    MessageBox.Show("Servizio non disponibile. Assicurarsi di averlo avviato");                    
                    Application.Current.Shutdown();

                }
            */
                LocationsList = new ObservableCollection<Location>();            
                    Llocation = _proxy.GetCurrentLocation();
                if (Llocation != null && Llocation.TasksList != null)
                    CountTasks = Llocation.TasksList.Count;
                else
                    CountTasks = 0;
                eraseTaskProperty();
            _sensibilitàSaved=Sensibilità=2;
            _checkedconnectedsaved = Checkedconnected = true;

            //codice per timer
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();


            /*     }
            else
            {
                MessageBox.Show("Servizio non attivo. Assicurarsi di averlo avviato");
                Application.Current.Shutdown();
            }*/
        }
        #endregion

        #region Command Handlers

        void GetStatus_f()
        {                       
                Llocation =  _proxy.GetCurrentLocation();
                if (Llocation!=null && Llocation.TasksList != null)
                    CountTasks = Llocation.TasksList.Count;
                else
                    CountTasks = 0;     
        }
       
        void StoreLocation_f ()
        {         
          if (NameLocationStr == null)
              NameLocationStr = "Location senza nome";
          if (DescriptionLocationStr == null)
              DescriptionLocationStr = "nessuna descrizione disponibile";                          
            Location loc_tmp=new Location();
            loc_tmp.Name = NameLocationStr;
            loc_tmp.Description = DescriptionLocationStr;
            loc_tmp.TasksList=new List<Task>();
            if(TasksList!=null)
            foreach (var startProcess in TasksList)
            {
                loc_tmp.TasksList.Add(startProcess);   
            }                                     
            _proxy.InsertLocation(loc_tmp);
            NameLocationStr = "";
            DescriptionLocationStr = "";
        }
        void ManagePrefered_f()
        {
             LocationsList = new ObservableCollection<Location>(_proxy.GetLocations());           
        }
      public  void ShowStats_f()
        {
            LocationsList =new ObservableCollection<Location>(_proxy.GetLocations());            
            StatisticList=new ObservableCollection<KeyValuePair<string, double>>();            
            foreach (Location location in LocationsList)
            {
                TimeSpan locsum=new TimeSpan();
                foreach (Statistic stat in location.StatisticsList)
                {
                    locsum = locsum + (stat.End - stat.Start);
                }
                double time = locsum.TotalMinutes;
                StatisticList.Add(new KeyValuePair<string, Double>(location.Name,time));                                
            }           
        }
       public  void AddTask_f()
        {
            if(TasksList==null)
                TasksList = new ObservableCollection<StartProcess>();   
          StartProcess Task=new StartProcess();
          Task.Delay = new TimeSpan(Hours,Minutes,Seconds);

          Task.ProcessName = Filename;
          Task.Type = Type;
          Task.Arguments = null;
          
          TasksList.Add(Task);
          eraseTaskProperty();
        }

       public void AddUrl_f()
       {
           if (TasksList == null)
               TasksList = new ObservableCollection<StartProcess>();
           StartProcess Task = new StartProcess();
           Task.Delay = new TimeSpan(Hours, Minutes, Seconds);
           Task.ProcessName = Url;
           Task.Type = Type;
           Task.Arguments = null;
           TasksList.Add(Task);
           eraseTaskProperty();
       }
	
		public void Setting_f()
		{
		    JimbeWiFi tmp=new JimbeWiFi();

		    Interfaces = new List<String>();//tmp.WiFiEnumInterfaces());

		    foreach (var wifi in tmp.WiFiEnumInterfaces())
		    {
                Interfaces.Add(wifi.InterfaceGuid.ToString());    
		    }
            
            Sensibilità = _sensibilitàSaved;
		    Checkedconnected = _checkedconnectedsaved;
		}
	
       public void NewTask_f()
        {
          //  AddTask newTask = new AddTask();
        //   newTask.ShowDialog();
        }


       public void NewUrl_f()
       {
        //   AddURL newUrl = new AddURL();
          // newUrl.ShowDialog();
       }
       
        #endregion

        #region functions
       public void delete_location(Location loc)
       {
           if (loc != null)
              
               foreach (var tmploc in LocationsList)
               {
                   if (tmploc.Name.Equals(loc.Name))
                   {
                       _proxy.DeleteLocation(tmploc);
                       LocationsList.Remove(tmploc);
                       break;
                   }
               }

       }

       public void ModifyTask(StartProcess tmp)
       {
           Delay = tmp.Delay;
           Hours = Delay.Hours;
           Minutes = Delay.Minutes;
           Seconds = Delay.Seconds;
           Type = tmp.Type;
           Filename = tmp.ProcessName;
           TaskIndex = TasksList.IndexOf(tmp);
       }
       public void update_Task()
       {
           StartProcess Task = new StartProcess();
           Task.Delay = new TimeSpan(Hours, Minutes, Seconds);
           Task.ProcessName = Filename;
           Task.Type = Type;
           Task.Arguments = null;
           TasksList.RemoveAt(TaskIndex);
           TasksList.Insert(TaskIndex, Task);
//           RaisePropertyChanged(() => TasksList);
           eraseTaskProperty();
       }
       public void DeleteTask(StartProcess tmp)
       {
           if (tmp != null)
           {
               TasksList.Remove(tmp);
           }
       }
       internal void ModifyLocation(Location loccc)
       {
           if (loccc != null)
           {
               NameLocationStr = loccc.Name;
               DescriptionLocationStr = loccc.Description;
               if (loccc.TasksList != null)
               {
                   TasksList = new ObservableCollection<StartProcess>();
                   foreach (var startProcess in loccc.TasksList)
                   {
                       TasksList.Add((StartProcess)startProcess);
                   }
               }
               LocationSensorList = loccc.SensorsList;
               LocationStatisticList = loccc.StatisticsList;               
               tmpupLocation = loccc;              
           }
       }
       public void SaveLocationChanges()
       {
           Location newLoc = new Location();
           newLoc.Name = NameLocationStr;
           newLoc.Description = DescriptionLocationStr;
           newLoc.SensorsList =  LocationSensorList;
           newLoc.StatisticsList = LocationStatisticList;
           if (TasksList != null)
           {
               newLoc.TasksList = new List<Task>(TasksList);
           }
           else
               newLoc.TasksList = null;
           _proxy.DeleteLocation(tmpupLocation);
           _proxy.InsertLocation(newLoc);
           NameLocationStr = "";
           DescriptionLocationStr = "";

       }

        public void eraseTaskProperty() 
        {
            Filename = "";
            Url = "http://";
            Type = JimbeWCF.DataContracts.Task.TaskType.Spot;
            Delay = System.TimeSpan.Zero;
            Hours = Delay.Hours;
            Minutes = Delay.Minutes;
            Seconds = Delay.Seconds;            
        }

        public void eraseLocationProperty()
        {
            NameLocationStr = "";
            DescriptionLocationStr = "";
            eraseTaskProperty();
            if (TasksList !=null)
                TasksList.Clear();
        }

        public void saveSettings()
        {
            _sensibilitàSaved = Sensibilità;
            _checkedconnectedsaved = Checkedconnected;
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _proxy = ProxyFactory.GetProxy(Properties.Settings.Default.Url_Client);
                GetStatus_f();
                ShowStats_f();            
            }
            catch (Exception)
            {
                MessageBox.Show("Servizio non disponibile, l'applicazione verrà chiusa");
                Application.Current.Shutdown();
            }
            
        }

        #endregion
       
    }
}