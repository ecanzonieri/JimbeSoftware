using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeApp.Helpers;
using JimbeWFC.DataContracts;
namespace JimbeApp.Models
{
    class WPF_WifiNetwork : NotificationObject
    {
        private WiFiNetwork _network;

        public string Ssid
        {
            get { return _network.Ssid; }
            set
            {
                if (this._network.Ssid != value)
                {
                    this._network.Ssid = value;
                    RaisePropertyChanged(() => Ssid);
                }
            }

        }

        public int SignalQuality
        {
            get { return this._network.SignalQuality; }
            set
            {
                if (this._network.SignalQuality != value)
                {
                    this._network.SignalQuality = value;
                    RaisePropertyChanged(() => SignalQuality);
                }
            }
        }
    }
}
