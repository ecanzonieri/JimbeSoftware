using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeApp.Helpers;
using JimbeWFC.DataContracts;
namespace JimbeApp.Models
{
    public class WPF_WifiConnectedSensor : NotificationObject
    {
        private WiFiConnectedSensor _sensor;
        public IList<WiFiNetwork> Connected
        {
            get { return _sensor.Connected; }
            set
            {
                if (this._sensor.Connected != value)
                {
                    this._sensor.Connected = value;
                    RaisePropertyChanged( () => Connected);
                }
            }
        }


    }
}
