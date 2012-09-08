using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Domain.Business;
using Ninject.Modules;

namespace JimbeService.IoC
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<LocationManager>().ToSelf().InSingletonScope();
        }
    }
}
