using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeApp.Helpers;
using JimbeWFC.DataContracts;

namespace JimbeApp.Models
{
    class WPF_WifiSensor : NotificationObject
    {
        private WiFiSensor _sensor;

        public IList<WiFiNetwork> Networks
        {
            get { return _sensor.Networks; }
            set
            {
                if (this._sensor.Networks != value)
                {
                    this._sensor.Networks = value;
                    RaisePropertyChanged(() => Networks);
                }
            }
        }
    }
}
