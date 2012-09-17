using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeApp.Helpers;
using JimbeWFC.DataContracts;

namespace JimbeApp.Models
{
    class WPF_StartProcess: NotificationObject
    {
        private StartProcess _startProcess;

        public string ProcessName
        {
           get { return _startProcess.ProcessName; }
            set
            {
                if (this._startProcess.ProcessName != value)
                {
                    this._startProcess.ProcessName = value;
                    RaisePropertyChanged(() => ProcessName);
                }
            }

        }

        
        public string Arguments
        {
            get { return _startProcess.Arguments; }
            set
            {

                if (this._startProcess.Arguments != value)
                {
                    this._startProcess.Arguments = value;
                    RaisePropertyChanged(() => Arguments);
                }
            }

        }
    }
}
