using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JimbeApp.Helpers;
using JimbeApp.Models;
using JimbeWFC.DataContracts;
using JimbeWFC.ServiceContract;

namespace JimbeApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Properties

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

        private int _asd;
        public int asd
        {
            get { return _asd; }
            set
            {
                if (this._asd != value)
                {
                    this._asd = value;
                    RaisePropertyChanged(() => asd);
                }
            }
        } 

        #endregion

        #region Commands

     //   public ICommand RefreshDateCommand { get { return new DelegateCommand(OnRefreshDate); } }       
public ICommand wifi {get { return  new DelegateCommand( wifi_ex);}}
public ICommand button_f { get { return new DelegateCommand(button_f_ex); } }
        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            ContentObjects = new ObservableCollection<Object>();
           
        }
        #endregion

        #region Command Handlers
        void wifi_ex ()
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
        }
        #endregion

     

    }
}