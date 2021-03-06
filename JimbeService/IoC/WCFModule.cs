﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeService.WCF;
using JimbeWCF.ServiceContract;
using Ninject.Modules;

namespace JimbeService.IoC
{
    public class WCFModule : NinjectModule
    {
        #region Overrides of NinjectModule

        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>().InSingletonScope();
        }

        #endregion
    }
}
