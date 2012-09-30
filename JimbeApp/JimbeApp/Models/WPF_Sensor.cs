using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeApp.Helpers;
using JimbeWFC.DataContracts;


namespace JimbeApp.Models
{
    public class WPF_Sensor : NotificationObject
    {
        private Sensor _Sensor;

        public double Weigth
        {
                get { return _Sensor.Weigth; }
            set
            {
                if (this._Sensor.Weigth != value)
                {
                    this._Sensor.Weigth = value;
                    RaisePropertyChanged(() => Weigth);
                }
            }
        }

    }
}
