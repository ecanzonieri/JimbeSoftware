using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using JimbeApp.Helpers;
using JimbeApp.Models;
using JimbeWFC.DataContracts;
using JimbeService.WCF;
using JimbeWFC.ServiceContract;

namespace JimbeApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Properties


        public WPF_Location Loc { get; set;}        
        private ObservableCollection<Object> _contentObjects;
        public ObservableCollection<Object> ContentObjects
        {
            get { return _contentObjects; }
            set
            {
                if (this._contentObjects != value)
                {
                    this._contentObjects = value;
                    RaisePropertyChanged(() => ContentObjects);
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

        }
        #endregion

        #region Command Handlers

        void GetStatus_f()
        {
           
            //var location = _proxy.GetCurrentLocation();
            //if (location != null)
            //{
            //    Loc = new WPF_Location(location);
            //}
            //else
            //{
            //    Loc = null;
            //}
        }


        void StoreLocation_f ()
        {
          
            
        }

        void ManagePrefered_f()
        {
           
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