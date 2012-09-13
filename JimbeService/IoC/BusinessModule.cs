using JimbeService.Business;
using Ninject.Modules;

namespace JimbeService.IoC
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServiceManager>().ToSelf().InSingletonScope();
        }
    }
}
