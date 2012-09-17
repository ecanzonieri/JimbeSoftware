using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeApp.Helpers;
using JimbeWFC.DataContracts;

namespace JimbeApp.Models
{
    class WPF_Statistic : NotificationObject
    {
        private Statistic _statistic;

        public DateTime Start
        {
            get { return _statistic.Start; }
            set
            {
                if(this._statistic.Start != value)
                {
                    this._statistic.Start = value;
                    RaisePropertyChanged(() => Start);
                }
            }
        }
        public DateTime End
        {
            get { return _statistic.End; }
            set
            {
                if (this._statistic.End != value)
                {
                    this._statistic.End = value;
                    RaisePropertyChanged(() => End);
                }
            }
        }

    }
}
