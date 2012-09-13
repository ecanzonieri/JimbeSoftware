using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeService.WCF;
using JimbeWFC.ServiceContract;
using Ninject.Modules;

namespace JimbeService.IoC
{
    class WCFModule : NinjectModule
    {
        #region Overrides of NinjectModule

        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>().InSingletonScope();
        }

        #endregion
    }
}
