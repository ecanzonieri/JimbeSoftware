using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using JimbeApp.Helpers;
using JimbeApp.Models;
using JimbeWFC.DataContracts;
using JimbeService.WCF;
using JimbeWFC.ServiceContract;



//per far partire il gestore servizio usare: wcftestclient dal prompt di visual studio2010
namespace JimbeApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Properties

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
        public string NameLocationStr { get; set; }
        public string DescriptionLocationStr { get; set; }

        private ObservableCollection<Location> _llocationList;
        public ObservableCollection<Location> lLocationList;

        private List<Location> _locationsList;
        public List<Location> LocationsList
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
     
        #endregion

        #region Commands
        public ICommand GetStatus { get { return new DelegateCommand(GetStatus_f); } }
        public ICommand StoreLocation { get { return new DelegateCommand(StoreLocation_f); } }
        public ICommand ManagePrefered { get { return new DelegateCommand(ManagePrefered_f); } }
        public ICommand ShowStats{get {return new DelegateCommand(ShowStats_f);}}      
        #endregion

        private ILocationService _proxy;

        #region Ctor
        public MainWindowViewModel()
        {
            _proxy = ProxyFactory.GetProxy();
            LocationsList=new List<Location>();  //tmp   
          //  Llocation =_proxy.GetCurrentLocation();   
        }
        #endregion

        #region Command Handlers

        void GetStatus_f()
        {                       
                Llocation =  _proxy.GetCurrentLocation();            

            /*if (LocationsList.Count >0)
            {
                Llocation = LocationsList[LocationsList.Count - 1]; //tmp         

                if (Llocation.TasksList != null)
                    CountTasks = Llocation.TasksList.Count;
                else
                    CountTasks = 0;

            }*/
        }

    public    void delete_location (Location loc)
        {
            _proxy.DeleteLocation(loc);
            //LocationsList.Remove(loc);
            //List<Location> tmp = LocationsList;
            //LocationsList = new List<Location>(tmp);
            //tmp.Clear();

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
            loc_tmp.SensorsList=new List<Sensor>();
            loc_tmp.StatisticsList=new List<Statistic>();
            _proxy.InsertLocation(loc_tmp);
        //  LocationsList.Add(loc_tmp);
            

        }

        void ManagePrefered_f()
        {
             LocationsList = new List<Location>(_proxy.GetLocations());            
            //List<Location> tmp=LocationsList;  
            //LocationsList = new List<Location>(tmp);
            //tmp.Clear();
        }
        void ShowStats_f()
        {
                 
        }

        #endregion

        #region example
        //   public ICommand RefreshDateCommand { get { return new DelegateCommand(OnRefreshDate); } }       
        //public ICommand wifi {get { return  new DelegateCommand( wifi_ex);}}
        //public ICommand button_f { get { return new DelegateCommand(button_f_ex); } }

        /*        void wifi_ex ()
        {   
            asd++;
            Label b = new Label();
            b.Width = 404;
            b.Content = asd.ToString();
            ContentObjects.Add(b);

            Button bb=new Button();
            bb.Content = "clear";
            bb.Command = button_f;
            ContentObjects.Add(bb);

        }
                void button_f_ex()
                {            
                   ContentObjects.Clear(); 
                }*/
        #endregion
    }
}